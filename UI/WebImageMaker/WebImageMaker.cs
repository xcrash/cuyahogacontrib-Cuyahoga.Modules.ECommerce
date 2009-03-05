using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Web.SessionState;

// BuildAsControlLibrary will only be defined when compiling manually. 
// It is defined as a compiler switch in Build_Switches.rsp
#if BuildAsControlLibrary
[assembly: WebResource("Guild.WebControls.WebImageMaker.css", "text/css")]
[assembly: WebResource("Guild.WebControls.WebImageMaker_canvas.js", "application/x-javascript")]
[assembly: WebResource("Guild.WebControls.WebImageMaker_normal.js", "application/x-javascript")]
#endif

namespace Guild.WebControls {

    public delegate void PurgeMethod(string directoryToClean);

    [DefaultProperty("CurrentImageUrl")]
    [ToolboxData("<{0}:WebImageMaker1 runat=\"server\"><{0}:WebImageMaker1>")]
    public class WebImageMaker : CompositeControl {
        public const string WebImageMakerID = "2a8b9574-6620-4df2-a36d-e8012bf624ba";
        public const string RawImageDirName = "raw";
        public const string CanvasImageDirName = "canvas";
        public const string ThumbnailImageDirName = "thumbnails";
        public const string WebImageDirName = "web";

        private HtmlImage targetImage;
        private Button uploadButton;
        private FileUpload upload;
        private Label lblInfo;
        private Label lblMessages;
        private HiddenField hiddenField;
        // the container for the canvas
        private HtmlGenericControl popupDiv;
        // container for thumbnails of images the user has recently worked on
        private HtmlGenericControl thumbnailsDiv;
        private HtmlInputButton thumbnailButton;
        // the image on which the user will "draw" the selection
        private HtmlImage canvas;
        // the div to provide the borders of the selected area
        private HtmlGenericControl selectionBox;
        private Button cancelSelection;
        private Button confirmSelection;
        private Label lblDebugInfo;

        private string imageWidth = "100";
        private string imageHeight = "100";
        private int intImageWidth = -1;
        private int intImageHeight = -1;
        private string serverImgID;
        private string originalSrc;
        private ControlMode controlMode = ControlMode.Normal;
        private bool hasChanged;
        private int canvasWidth;
        private int canvasHeight;
        private string workingDirectory;
        private WebImageFormat webImageFormat = WebImageFormat.Jpg;
        private WebImageQuality webImageQuality = WebImageQuality.High;
        private int rawWidth;
        private int rawHeight;
        private string canvasImageName;
        private string webImageName;
        private string handlerPath;
        private float aspectRatio = 0;
        private int thumbnailSize = 64;

        private int isServingImageState = -1; // -1 indicates unset
        private bool IsServingImage {
            get {
                if (isServingImageState == -1) {
                    try {
                        isServingImageState = (HttpContext.Current.Request.QueryString["mode" + KeySuffix] != null) ? 1 : 0;
                    } catch {
                        isServingImageState = 0; // probably being called by the designer
                    }
                }
                return (isServingImageState == 1);
            }
        }

        private string KeySuffix {
            get { return "_" + WebImageMakerID; }
        }

        protected override void OnInit(EventArgs e) {

            //Set a default from the web config
            if (string.IsNullOrEmpty(workingDirectory)) {
                workingDirectory = Cuyahoga.Modules.ECommerce.Util.WebHelper.GetImageWorkingDirectory();
            }

            // here's the dirty code to hijack the page request and
            // serve out the canvas image... If you don't like it,
            // use the handler
            if (Page.Request.QueryString["mode" + KeySuffix] != null) {
                WebImageMakerImageHelper helper = new WebImageMakerImageHelper(workingDirectory, KeySuffix);
                helper.serveImage();
            }

            Page.RegisterRequiresControlState(this);
            base.OnInit(e);
        }

        protected override void LoadControlState(object savedState) {
            string[] state = (string[])savedState;
            if (!Int32.TryParse(state[0], out intImageWidth)) intImageWidth = -1;
            if (!Int32.TryParse(state[1], out intImageHeight)) intImageHeight = -1;
            serverImgID = state[2];
            originalSrc = state[3];
            controlMode = (ControlMode)Enum.Parse(typeof(ControlMode), state[4]);
            hasChanged = (state[5] == "1");
            if (!Int32.TryParse(state[6], out canvasWidth)) canvasWidth = 0;
            if (!Int32.TryParse(state[7], out canvasHeight)) canvasHeight = 0;
            workingDirectory = state[8];
            webImageFormat = (WebImageFormat)Enum.Parse(typeof(WebImageFormat), state[9]);
            webImageQuality = (WebImageQuality)Enum.Parse(typeof(WebImageQuality), state[10]);
            if (!Int32.TryParse(state[11], out rawWidth)) rawWidth = 0;
            if (!Int32.TryParse(state[12], out rawHeight)) rawHeight = 0;
            canvasImageName = state[13];
            webImageName = state[14];
            handlerPath = state[15];
            if (!Single.TryParse(state[16], out aspectRatio)) aspectRatio = 0;
            if (!Int32.TryParse(state[17], out thumbnailSize)) thumbnailSize = 64;
        }

        protected override object SaveControlState() {
            return new string[] { 
                intImageWidth.ToString(), 
                intImageHeight.ToString(), 
                serverImgID, 
                originalSrc, 
                controlMode.ToString(), 
                (hasChanged ? "1" : "0"),
                canvasWidth.ToString(), 
                canvasHeight.ToString(),
                workingDirectory,
                webImageFormat.ToString(), 
                webImageQuality.ToString(),
                rawWidth.ToString(), 
                rawHeight.ToString(),
                canvasImageName, 
                webImageName, 
                handlerPath,
                aspectRatio.ToString(),
                thumbnailSize.ToString()
            };
        }


        private IImageProvider __imageProvider;
        private IImageProvider ImageProvider {
            get {
                if (__imageProvider == null) {
                    // For now we'll just instantiate our implementation here as it's the only
                    // one. In the future the ImageProvider could be implemented along 
                    // provider model lines
                    __imageProvider = new ImageProviderImpl();
                    __imageProvider.WorkingDirectory = this.workingDirectory;
                    __imageProvider.ServerID = this.serverImgID;
                    __imageProvider.ThumbnailSize = this.thumbnailSize;
                    __imageProvider.PurgeStrategy = this.purgeStrategy;
                }
                return __imageProvider;
            }
        }

        private PurgeMethod purgeStrategy;
        public PurgeMethod PurgeStrategy {
            get { return purgeStrategy; }
            set { purgeStrategy = value; }
        }


        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("100")]
        [Themeable(false)]
        public string ImageWidth {
            get { return imageWidth; }
            set {
                if (Int32.TryParse(value, out intImageWidth) && intImageWidth > 0 && !IsServingImage) {
                    EnsureChildControls();
                    targetImage.Width = intImageWidth;
                    imageWidth = intImageWidth.ToString();
                } else {
                    imageWidth = "*";
                    intImageWidth = -1;
                }
            }
        }


        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("100")]
        [Themeable(false)]
        public string ImageHeight {
            get { return imageHeight; }
            set {
                if (Int32.TryParse(value, out intImageHeight) && intImageHeight > 0 && !IsServingImage) {
                    EnsureChildControls();
                    targetImage.Height = intImageHeight;
                    imageHeight = intImageHeight.ToString();
                } else {
                    imageHeight = "*";
                    intImageHeight = -1;
                }
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("Change...")]
        [Themeable(false)]
        public string UploadButtonText {
            get {
                EnsureChildControls();
                return uploadButton.Text;
            }
            set {
                if (!IsServingImage) // child controls won't be instantiated
                {
                    EnsureChildControls();
                    uploadButton.Text = value;
                }
            }
        }


        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("Cancel")]
        [Themeable(false)]
        public string CancelButtonText {
            get {
                EnsureChildControls();
                return cancelSelection.Text;
            }
            set {
                if (!IsServingImage) // child controls won't be instantiated
                {
                    EnsureChildControls();
                    cancelSelection.Text = value;
                }
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("Confirm Selection")]
        [Themeable(false)]
        public string ConfirmButtonText {
            get {
                EnsureChildControls();
                return confirmSelection.Text;
            }
            set {
                if (!IsServingImage) // child controls won't be instantiated
                {
                    EnsureChildControls();
                    confirmSelection.Text = value;
                }
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [Themeable(false)]
        public string WorkingDirectory {
            get {
                return workingDirectory;
            }
            set {
                workingDirectory = value;
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [Themeable(false)]
        public string HandlerPath {
            get {
                return handlerPath;
            }
            set {
                handlerPath = value;
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue(WebImageFormat.Jpg)]
        [Themeable(false)]
        public WebImageFormat Format {
            get {
                return webImageFormat;
            }
            set {
                webImageFormat = value;
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue(WebImageQuality.High)]
        [Themeable(false)]
        public WebImageQuality Quality {
            get {
                return webImageQuality;
            }
            set {
                webImageQuality = value;
            }
        }


        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue(64)]
        [Themeable(false)]
        public int ThumbnailSize {
            get {
                return thumbnailSize;
            }
            set {
                thumbnailSize = value;
            }
        }


        [Bindable(false)]
        [Category("Appearance")]
        [Themeable(false)]
        public string ImageUrl {
            get { return originalSrc; }
            set { originalSrc = value; }
        }

        public string WebImagePath {
            get {
                if (controlMode == ControlMode.Changed) {
                    return Path.Combine(Path.Combine(workingDirectory, WebImageDirName), webImageName);
                } else {
                    return null;
                }
            }
        }

        // it might be useful to have this available to other controls on the page.
        public string CurrentImageUrl {
            get {
                return targetImage.Src;
            }
        }

        protected override void OnPreRender(EventArgs e) {
            String cssUrl, jsCanvasUrl, jsThumbsUrl;

#if BuildAsControlLibrary
            cssUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "Guild.WebControls.WebImageMaker.css");
#else
            cssUrl = Page.ResolveUrl("~/WebImageMaker.css");
#endif
            string cssLink = "<link href=\"" + cssUrl + "\" rel=\"stylesheet\" type=\"text/css\" />";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "WebImageMaker_Control_css",
                    "<link href=\"" + cssUrl + "\" rel=\"stylesheet\" type=\"text/css\" />", false);

            if (controlMode == ControlMode.Canvas) {
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), this.ClientID, getInitScript(), true);
#if BuildAsControlLibrary
                jsCanvasUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "Guild.WebControls.WebImageMaker_canvas.js");
#else
                jsCanvasUrl = Page.ResolveUrl("~/WebImageMaker_canvas.js");
#endif
                Page.ClientScript.RegisterClientScriptInclude(
                    "WebImageMaker_Control_Canvas_js", jsCanvasUrl);
            } else {
#if BuildAsControlLibrary
                jsThumbsUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "Guild.WebControls.WebImageMaker_normal.js");
#else
                jsThumbsUrl = Page.ResolveUrl("~/WebImageMaker_normal.js");
#endif
                Page.ClientScript.RegisterClientScriptInclude(
                    "WebImageMaker_Control_Thumbs_js", jsThumbsUrl);

                //make sure canvas script is loaded first



            }
        }


        private string getInitScript() {
            string s = @"
function init{0}()
{{
    initialise('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}');
}}

window.onload = init{0};

";
            return String.Format(s,
                this.ClientID, popupDiv.ClientID, canvas.ClientID, selectionBox.ClientID,
                imageWidth, imageHeight, targetImage.ClientID, confirmSelection.ClientID,
                lblDebugInfo.ClientID);

        }

        protected override void CreateChildControls() {
            if (IsServingImage) {
                this.ChildControlsCreated = true;
                return;
            }

            targetImage = new HtmlImage();
            uploadButton = new Button();
            upload = new FileUpload();
            lblInfo = new Label();
            lblMessages = new Label();
            hiddenField = new HiddenField();
            popupDiv = new HtmlGenericControl("div");
            thumbnailsDiv = new HtmlGenericControl("div");
            thumbnailButton = new HtmlInputButton();
            canvas = new HtmlImage();
            selectionBox = new HtmlGenericControl("div");
            cancelSelection = new Button();
            confirmSelection = new Button();
            lblDebugInfo = new Label();

            hiddenField.ID = "hidden";
            targetImage.ID = "img";
            popupDiv.Attributes.Add("class", "webImageMaker_popup");
            popupDiv.ID = this.ID + "_popup";
            canvas.Attributes.Add("class", "webImageMaker_canvas");
            canvas.ID = "canvas";
            selectionBox.Attributes.Add("class", "webImageMaker_selection");
            selectionBox.ID = "selection";
            cancelSelection.Attributes.Add("class", "webImageMaker_cancel");
            cancelSelection.ID = "cancel";
            confirmSelection.Attributes.Add("class", "webImageMaker_confirm");
            confirmSelection.ID = "confirm";
            cancelSelection.Click += new EventHandler(cancelSelection_Click);
            confirmSelection.UseSubmitBehavior = false;
            confirmSelection.OnClientClick = "storeSelectionInfo('" + hiddenField.ClientID + "')";
            confirmSelection.Click += new EventHandler(confirmSelection_Click);

            foreach (string thumbnailFilename in SessionImages) {
                ImageButton thumbBtn = getThumbnailButton(thumbnailFilename);
                thumbBtn.Command += new CommandEventHandler(btnThumb_Command);
                thumbnailsDiv.Controls.Add(thumbBtn);
            }

            uploadButton.UseSubmitBehavior = false;
            uploadButton.OnClientClick = "setViewportDimensions('" + hiddenField.ClientID + "')";
            uploadButton.Click += new EventHandler(this.uploadButton_Click);
            this.Controls.Add(popupDiv);
            this.Controls.Add(hiddenField);
            this.Controls.Add(targetImage);
            this.Controls.Add(lblInfo);

            this.Controls.Add(upload);
            this.Controls.Add(uploadButton);
            popupDiv.Controls.Add(canvas);
            popupDiv.Controls.Add(selectionBox);
            popupDiv.Controls.Add(cancelSelection);
            popupDiv.Controls.Add(confirmSelection);
            popupDiv.Controls.Add(lblDebugInfo);
            this.Controls.Add(lblMessages);
            lblMessages.Visible = false;
            this.ChildControlsCreated = true;
        }
        private ImageButton getThumbnailButton(string thumbnailFilename) {
            ImageButton btnThumb = new ImageButton();
            btnThumb.ImageUrl = getImageSource(thumbnailFilename, "thumbnail");
            btnThumb.CommandName = "ThumbnailSelect";
            btnThumb.CommandArgument = thumbnailFilename;
            return btnThumb;
        }



        protected override void Render(HtmlTextWriter writer) {
            AddAttributesToRender(writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            if (this.controlMode == ControlMode.Canvas) {
                confirmSelection.OnClientClick = "storeSelectionInfo('" + hiddenField.ClientID + "')";
                popupDiv.RenderControl(writer);
            }
            hiddenField.ID = "hidden";
            hiddenField.RenderControl(writer);
            if (this.controlMode == ControlMode.Normal) {
                targetImage.Src = originalSrc;
            }
            targetImage.RenderControl(writer);
            writer.WriteBreak();
            if (controlMode != ControlMode.Canvas) {
                //lblInfo.Text = "Image Maker Width: " + (imageWidth == "*" ? "arbitrary" : imageWidth) + " " + "Height: " + (imageHeight == "*" ? "arbitrary" : imageHeight) + " ";
                lblInfo.RenderControl(writer);
                writer.WriteBreak();

            }
            upload.RenderControl(writer);
            writer.WriteBreak();
            uploadButton.OnClientClick = "setViewportDimensions('" + hiddenField.ClientID + "')";
            uploadButton.RenderControl(writer);
            lblMessages.RenderControl(writer);
            thumbnailButton.Attributes.Add("onclick", "showThumbnailDiv('" + thumbnailsDiv.ClientID + "');");
            writer.RenderEndTag();
        }



        void btnThumb_Command(object sender, CommandEventArgs e) {
            bool imageOK = ImageProvider.UseThumbnailFile(
                e.CommandArgument.ToString(), out serverImgID, out rawWidth, out rawHeight);
            if (!imageOK) {
                lblMessages.Text = "Could not find the uploaded image corresponding to the thumbnail.";
                lblMessages.Visible = true;
                return;
            }
            UseRaw();
        }



        void uploadButton_Click(object sender, EventArgs e) {
            /*
             * see if we have a file in the form.
             * If we don't, display a notification message.
             * if we do and it's too small, display a notification message
             *  *** Actually we don't care if it's too small - we'll just scale it up
             * If we do and it's exactly the right size, use this image and go directly to changed.
             * If we do and it's bigger, go into Canvas mode and send back the necessary stuff
             */

            // do we have a file?
            if (!upload.HasFile) {
                lblMessages.Text = "No file present. Might be too big.";
                lblMessages.Visible = true;
                return;
            }

            // is it an image?
            string thumbnailFileName;
            bool imageOK = ImageProvider.SaveRaw(upload.PostedFile, out serverImgID,
                out rawWidth, out rawHeight, out thumbnailFileName);
            if (!imageOK) {
                lblMessages.Text = "File is not an image that the system understands.";
                lblMessages.Visible = true;
                return;
            }

            // we've got this far, so make a thumbnail and store the Guid in the user's 
            // session, so they can reuse the image later without having to upload it again.
            SessionImages.Add(thumbnailFileName);

            UseRaw();
        }

        private void UseRaw() {
            if (intImageWidth == rawWidth && intImageHeight == rawHeight) {
                // the image is already the right size - we'll just save it as the web image,
                // taking the required format and quality into consideration                
                webImageName = ImageProvider.SaveRawImageAsWebImage(webImageFormat, webImageQuality);
                targetImage.Src = getImageSource(webImageName, "web");
                controlMode = ControlMode.Changed;
                return;
            }
            CanvasFromRaw();
        }

        private void CanvasFromRaw() {
            // we're going to need to do some further work with this image so let's work 
            // out a few things about it
            aspectRatio = (float)rawWidth / (float)rawHeight;

            // the image is not the right size so we need to make a canvas
            string[] clientDimensions = hiddenField.Value.Split(new char[] { ',' });
            // we'll allow the canvas to be up to 80% of the user's current browser window size:
            int clientX = 0;
            int clientY = 0;
            try {
                clientX = Convert.ToInt32(clientDimensions[0]) * 4 / 5;
                clientY = Convert.ToInt32(clientDimensions[1]) * 4 / 5;
            } catch {

            }
            float clientRatio = (float)clientX / (float)clientY;

            // which is the dimension that should constrain the canvas size?
            if (clientRatio > aspectRatio) {
                // y axis constrains the canvas size
                canvasHeight = clientY;
                canvasWidth = rawWidth * clientY / rawHeight;
            } else {
                canvasWidth = clientX;
                canvasHeight = rawHeight * clientX / rawWidth;
            }

            canvasImageName = ImageProvider.CreateCanvas(
                webImageFormat, webImageQuality,
                canvasWidth, canvasHeight);
            canvas.Src = getImageSource(canvasImageName, "canvas");
            canvas.Width = canvasWidth;
            canvas.Height = canvasHeight;
            controlMode = ControlMode.Canvas;
        }


        void confirmSelection_Click(object sender, EventArgs e) {
            string[] clientDimensions = hiddenField.Value.Split(new char[] { ',' });
            int x = Convert.ToInt32(clientDimensions[0]);
            int y = Convert.ToInt32(clientDimensions[1]);
            int w = Convert.ToInt32(clientDimensions[2]);
            int h = Convert.ToInt32(clientDimensions[3]);
            // now we have the x,y,w,h of the selection relative to the canvas, so
            // we have to scale the selection so that it is a selection from the 
            // original raw image:
            float scaleFactor = (float)canvasWidth / (float)rawWidth;
            Rectangle transformedSelection = new Rectangle(
                (int)(x / scaleFactor),
                (int)(y / scaleFactor),
                (int)(w / scaleFactor),
                (int)(h / scaleFactor));
            // transformedSelection now represents the user's selected crop on the 
            // raw image rather than the canvas image

            // now determine what the dimensions of the final image should be. If 
            // ImageWidth and ImageHeight were both set then we already know, but
            // if one of them was "*" then we need to work out what it should
            // proportionally be from the user's selected crop shape:
            float selectionAspectRatio = (float)w / (float)h;
            int reqdWidth = intImageWidth;
            int reqdHeight = intImageHeight;
            // these should never be both <= 0
            if (reqdWidth <= 0) {
                reqdWidth = (int)(selectionAspectRatio * reqdHeight);
            } else if (reqdHeight <= 0) {
                reqdHeight = (int)(reqdWidth / selectionAspectRatio);
            }

            // now we have everything we need: what area (transformedSelection) to crop 
            // out of the raw image, and what dimensions this cropped area should be
            // resized to:
            webImageName = ImageProvider.CropAndScale(
                transformedSelection,
                webImageFormat, webImageQuality,
                reqdWidth, reqdHeight);
            targetImage.Src = getImageSource(webImageName, "web");
            controlMode = ControlMode.Changed;
        }

        void cancelSelection_Click(object sender, EventArgs e) {
            controlMode = hasChanged ? ControlMode.Changed : ControlMode.Normal;
        }

        private string getImageSource(string imageName, string mode) {
            String queryString = "?mode=" + mode + "&img=" + imageName;
            if (String.IsNullOrEmpty(handlerPath)) {
                // put a unique identifier into the querystring args to avoid mixing
                // our params with someone else's
                queryString = queryString.Replace("=", KeySuffix + "=");
                return HttpContext.Current.Request.FilePath.Substring(
                    HttpContext.Current.Request.FilePath.LastIndexOf("/") + 1) + queryString;
            } else {
                return Page.ResolveUrl(HandlerPath) + queryString;
            }
        }

        private List<string> SessionImages {
            get {
                List<string> sessionImages = new List<string>();
                try {
                    if (HttpContext.Current != null) // will be null if called by designer
                    {
                        string key = "WEBIMAGEMAKER_IMAGES";
                        sessionImages = (List<string>)HttpContext.Current.Session[key];
                        if (sessionImages == null) {
                            sessionImages = new List<string>();
                            HttpContext.Current.Session[key] = sessionImages;
                        }
                    }
                } catch { }
                return sessionImages;
            }
        }
    }


    public enum ControlMode {
        Normal, Canvas, Changed
    }

    public enum WebImageFormat {
        Gif, Jpg, Png
    }

    public enum WebImageQuality {
        High, Medium, Low
    }
}
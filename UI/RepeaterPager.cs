using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Koutny.WebControls
{
	/// <summary>
	/// Repeater page changed argument.
	/// </summary>
	public class RepeaterPageChangedEventArgs : EventArgs
	{
		public RepeaterPageChangedEventArgs(object source, int index)
		{
			_CommandSource				=	source;
			_NewPageIndex				=	index;
		}

		
		private object					_CommandSource;
		private int						_NewPageIndex;

		/// <summary>
		/// Zdroj stránkování.
		/// </summary>
		public object					CommandSource
		{
			get {	return _CommandSource;	}
		}

		/// <summary>
		/// Nový index stránky.
		/// </summary>
		public int						NewPageIndex
		{
			get {	return _NewPageIndex;	}
		}
	}

	
	/// <summary>
	/// Handler used when repeater page changed event occure.
	/// </summary>
	public delegate void RepeaterPageChangedEventHandler(object sender, RepeaterPageChangedEventArgs args);

	/// <summary>
	/// Kind of data paging.
	/// </summary>
	public enum RepeaterPagingType : int
	{
		/// <summary>
		/// Classic way.
		/// </summary>
		Classic			=	1,

		/// <summary>
		/// Specifying virtual items count, suitable when displaying huge amount of data.
		/// </summary>
		VirtualItems	=	2
	}


	/// <summary>
	/// Pager's type.
	/// </summary>
	public enum RepeaterPagersType : int
	{
		OnlyNumeric				=	1,

		OnlyWords				=	2,

		NumbersBetweenWords		=	3,

		NumbersBehindWords		=	4,

		NumbersBeforeWords		=	5
	}


	/// <summary>
	/// Class that represents simple paging item.
	/// </summary>
	[ToolboxItem(false)]
	public class RepeaterPagerItem : Control, INamingContainer
	{
		public RepeaterPagerItem(int pageIndex)
		{
			_PageIndex					=	pageIndex;
		}


		private int						_PageIndex;

		/// <summary>
		/// Index of paging item page.
		/// </summary>
		public int						PageIndex
		{
			get {	return _PageIndex;	}
		}


		/// <summary>
		/// Text that represents paging item page.
		/// </summary>
		public string					Value
		{
			get {	return (PageIndex + 1).ToString();	}
		}

		/// <summary>
		/// Bubbling of uncatched event.
		/// </summary>
		protected override bool			OnBubbleEvent(object source, EventArgs args)
		{
			if (args != null && args is CommandEventArgs)
			{
				if (((CommandEventArgs)args).CommandName == RepeaterPager.PageCommandName)
				{
					this.RaiseBubbleEvent(this, new CommandEventArgs(RepeaterPager.PageCommandName, this.PageIndex));
					return true;
				}
			}

			return base.OnBubbleEvent (source, args);
		}
	}


	/// <summary>
	/// Class that allows paging a repeater.
	/// </summary>
	/// <remarks>Must be placed in page out of repeater to avoid throwing OutOfStackException</remarks>
	[ParseChildren(true)]
	[Designer(typeof(RepeaterPagerDesigner))]
	[ToolboxData("<{0}:RepeaterPager runat=\"server\"></{0}:LoginControl>")]
	[ToolboxItem(true)]
	public class RepeaterPager : Control, INamingContainer
	{
		/// <summary>
		/// Name of paging command.
		/// </summary>
		public const string				PageCommandName = "Page";

		/// <summary>
		/// Occurs when page is being changed.
		/// </summary>
		public event RepeaterPageChangedEventHandler
										PageIndexChanged;

		private object					_DataSource;

		private ITemplate				_NextPagerTemplate;
		private ITemplate				_PreviousPagerTemplate;
		private ITemplate				_NumericPagerTemplate;
		private ITemplate				_SelectedNumericPagerTemplate;
		private ITemplate				_EmptyPagerTemplate;

		#region Public properties

		/// <summary>
		/// Paged Repeater id.
		/// </summary>
		[Browsable(true)]
		public string					RepeaterID
		{
			get {	return (string)ViewState["RepeaterID"];	}
			set {	ViewState["RepeaterID"]		= value;	}
		}


		/// <summary>
		/// Index of currently renderred page.
		/// </summary>
		public int						PageIndex
		{
			get 
			{	
				if (ViewState["PageIndex"] == null)
					ViewState["PageIndex"] = 0;
				return (int)ViewState["PageIndex"];	}
			set 
			{	
				if (value < 0)
					throw new ArgumentException("Index of current page must be non-negative.");

				ViewState["PageIndex"] = value;	}
		}


		/// <summary>
		/// Size of page, must be greater than zero.
		/// </summary>
		/// <remarks>Default value is 10</remarks>
		[Browsable(true)]
		public int						PageSize
		{
			get 
			{	
				if (ViewState["PageSize"] == null)
					ViewState["PageSize"] = 10;
				return (int)ViewState["PageSize"];	}
			set 
			{	
				if (value <= 0)
					throw new ArgumentException("Size of page must be greater than zero.");

				ViewState["PageSize"] = value;	}
		}


		/// <summary>
		/// Paging type.
		/// </summary>
		[Browsable(true)]
		public RepeaterPagingType		PagingType
		{
			get 
			{	
				if (ViewState["RepeaterPagingType"] == null)
					ViewState["RepeaterPagingType"] = RepeaterPagingType.Classic;
				return (RepeaterPagingType)ViewState["RepeaterPagingType"];
			}
			set {	ViewState["RepeaterPagingType"] = value;	}
		}


		/// <summary>
		/// Type of used pagers.
		/// </summary>
		[Browsable(true)]
		public RepeaterPagersType		PagersType
		{
			get 
			{	
				if (ViewState["PagersType"] == null)
					ViewState["PagersType"] = RepeaterPagersType.NumbersBetweenWords;
				return (RepeaterPagersType)ViewState["PagersType"];
			}
			set {	ViewState["PagersType"] = value;	}
		}


		/// <summary>
		/// Maximum number of displayed numeric pagers.
		/// </summary>
		/// <remarks>Default value is 5</remarks>
		[Browsable(true)]
		public int						MaxNumericPagers
		{
			get 
			{	
				if (ViewState["MaxNumericPagers"] == null)
					ViewState["MaxNumericPagers"] = 5;
				return (int)ViewState["MaxNumericPagers"];	}
			set {	ViewState["MaxNumericPagers"] = value;	}
		}


		/// <summary>
		/// Virtual items count that represents data source size when using this type of data delivery.
		/// </summary>
		[Browsable(true)]
		public int						VirtualItemsCount
		{
			get 
			{	
				if (ViewState["VirtualItemsCount"] == null)
					ViewState["VirtualItemsCount"] = 0;
				return (int)ViewState["VirtualItemsCount"];	}
			set {	ViewState["VirtualItemsCount"] = value;	}
		}


		/// <summary>
		/// Minimial size of space between numeric pagers and first/last pager.
		/// </summary>
		/// <remarks>Default value is 2</remarks>
		[Browsable(true)]
		public int						EmptySpaceSize
		{
			get 
			{
				if (ViewState["EmptySpaceSize"] == null)
					ViewState["EmptySpaceSize"] = 2;
				return (int)ViewState["EmptySpaceSize"];	}
			set 
			{
				if (value < 1)
					throw new ArgumentException("Empty space size must be greater or equal to 1.");
				ViewState["EmptySpaceSize"]	=	value;		}
		}


		/// <summary>
		/// Text placed before numeric pagers as literal.
		/// </summary>
		[Browsable(true)]
		public string					TextBeforeNumericPagers
		{
			get {	return (string)ViewState["TextBeforeNumericPagers"];	}
			set {	ViewState["TextBeforeNumericPagers"]	=	value;		}
		}


		/// <summary>
		/// Text placed behind numeric pagers as literal.
		/// </summary>
		[Browsable(true)]
		public string					TextBehindNumericPagers
		{
			get {	return (string)ViewState["TextBehindNumericPagers"];	}
			set {	ViewState["TextBehindNumericPagers"]	=	value;		}
		}


		/// <summary>
		/// Data source that implements ICollection interface.
		/// </summary>
		public object					DataSource
		{
			get {	return _DataSource;	}
			set 
			{
				if (value != null) 
				{
				
					if (!(value is ICollection))
						throw new ArgumentException("Data source doesn't implement ICollection.");

					_DataSource			=	value;
				}
				else
					_DataSource			=	null;
			}
		}


		/// <summary>
		/// Datasource size for inner purposes.
		/// </summary>
		protected virtual int			DataSourceSize
		{
			get 
			{	
				if (ViewState["DataSourceSize"] != null)
					return (int)ViewState["DataSourceSize"];
				else if (PagingType == RepeaterPagingType.Classic) {
				
					if (DataSource != null)
						return ((ICollection)DataSource).Count;
					else
						return 0;
				}
				else if (PagingType == RepeaterPagingType.VirtualItems)
					return this.VirtualItemsCount;
				else
					return 0;
			}
		}


		/// <summary>
		/// Template of "next" word pager.
		/// </summary>
		[TemplateContainer(typeof(RepeaterPagerItem)),
		Browsable(false),
		PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate				NextPagerTemplate
		{
			get {	return _NextPagerTemplate;	}
			set {	_NextPagerTemplate = value;	}
		}


		/// <summary>
		/// Template of "previous" word pager.
		/// </summary>
		[TemplateContainer(typeof(RepeaterPagerItem))]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate				PreviousPagerTemplate
		{
			get {	return _PreviousPagerTemplate;	}
			set {	_PreviousPagerTemplate = value;	}
		}


		/// <summary>
		/// Template of numeric pager.
		/// </summary>
		[TemplateContainer(typeof(RepeaterPagerItem))]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate				NumericPagerTemplate
		{
			get {	return _NumericPagerTemplate;	}
			set {	_NumericPagerTemplate = value;	}
		}


		/// <summary>
		/// Template of selected numeric pager.
		/// </summary>
		[TemplateContainer(typeof(RepeaterPagerItem))]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate				SelectedNumericPagerTemplate
		{
			get {	return _SelectedNumericPagerTemplate;	}
			set {	_SelectedNumericPagerTemplate = value;	}
		}


		/// <summary>
		/// Template of EmptySpace-numberic pager.
		/// </summary>
		[TemplateContainer(typeof(RepeaterPagerItem))]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate				EmptyPagerTemplate
		{
			get {	return _EmptyPagerTemplate;	}
			set {	_EmptyPagerTemplate = value;	}
		}


		#endregion Public properties


		/// <summary>
		/// Gets index of lower border in data source.
		/// </summary>
		public virtual int				GetPageLowerBorder()
		{
			return PageIndex * PageSize;
		}


		/// <summary>
		/// Gets index of upper border in data source.
		/// </summary>
		public virtual int				GetPageUpperBorder()
		{
			return ((PageIndex+1) * PageSize) - 1;
		}


		/// <summary>
		/// Returns paged repeater.
		/// </summary>
		/// <param name="id">Identificator</param>
		protected virtual Repeater		GetRepeater(string id)
		{
			if (id == null)
				throw new NullReferenceException("Identificator is null.");

			Control			tempParent	=	this.Parent;
			Control			tempCtl		=	null;

			//	parent iteration.
			while (tempParent != null) 
			{
			
				tempCtl					=	tempParent.FindControl(id);
				if (tempCtl != null && tempCtl is Repeater) 
				{
				
					break;
				}

				tempParent				=	tempParent.Parent;
			}

			if (tempCtl != null && tempCtl is Repeater)
				return (Repeater)tempCtl;
			else
				throw new ApplicationException("RepeaterID is invalid.\nValue: " + id);
		}


		/// <summary>
		/// Bubbling of uncatched event.
		/// </summary>
		protected override bool			OnBubbleEvent(object source, EventArgs args)
		{
			if (args != null && args is CommandEventArgs) 
			{
				CommandEventArgs evArgs	=	(CommandEventArgs)args;
				if (evArgs.CommandName == PageCommandName) 
				{
					this.OnPageIndexChanged(new RepeaterPageChangedEventArgs(source, (int)evArgs.CommandArgument));
					return true;
				}
			}

			return base.OnBubbleEvent (source, args);
		}


		/// <summary>
		/// Page change event initialization.
		/// </summary>
		/// <param name="args">Arguments</param>
		protected virtual void			OnPageIndexChanged(RepeaterPageChangedEventArgs args)
		{
			if (PageIndexChanged != null)
				PageIndexChanged(this, args);
		}


		/// <summary>
		/// Child controls creation.
		/// </summary>
		protected override void			CreateChildControls()
		{
			base.CreateChildControls ();
			Controls.Clear();

			CreatePagers(this.DataSourceSize, this.PageSize, this.PagersType);
		}


		/// <summary>
		/// Data binding.
		/// </summary>
		protected override void			OnDataBinding(EventArgs e)
		{
			base.OnDataBinding (e);

			Controls.Clear();
			this.ClearChildViewState();

			if (DataSource != null) 
			{
				if (PagingType == RepeaterPagingType.Classic) {
				
					this.CreatePagers(((ICollection)DataSource).Count, this.PageSize, PagersType);
					SetDataSourceSize(((ICollection)DataSource).Count);
				}
				else if (PagingType == RepeaterPagingType.VirtualItems) {
				
					this.CreatePagers(this.VirtualItemsCount, this.PageSize, PagersType);
					SetDataSourceSize(this.VirtualItemsCount);
				}

				Repeater		rep		=	GetRepeater(this.RepeaterID);
				rep.DataSource			=	GetDataSource((ICollection)this.DataSource, this.PageIndex, this.PageSize, this.PagingType);
				rep.DataBind();
			}

			this.ChildControlsCreated	=	true;
		}


		/// <summary>
		/// Sets inner variable that represents datasource size.
		/// </summary>
		/// <param name="size">Data source size</param>
		protected virtual void			SetDataSourceSize(int size)
		{
			if (size < 0)
				throw new ArgumentException("DataSource size must be non-negative.");

			ViewState["DataSourceSize"]	=	size;
		}


		/// <summary>
		/// Main pagers creation.
		/// </summary>
		/// <param name="itemsCount">Size of data source</param>
		/// <param name="pageSize">Page size</param>
		/// <param name="type">Pagers type</param>
		protected virtual void			CreatePagers(int itemsCount, int pageSize, RepeaterPagersType type)
		{
			if (type == RepeaterPagersType.OnlyWords) {
			
				CreatePreviousPager(this.PageIndex);
				CreateNextPager(this.PageIndex, pageSize);
			}
			else if (type == RepeaterPagersType.OnlyNumeric) {
				
				CreateNumericPagers(this.PageIndex, this.PageSize, this.MaxNumericPagers);
			}
			else if (type == RepeaterPagersType.NumbersBetweenWords) {
				
				CreatePreviousPager(this.PageIndex);
				CreateNumericPagers(this.PageIndex, this.PageSize, this.MaxNumericPagers);
				CreateNextPager(this.PageIndex, pageSize);
			}
			else if (type == RepeaterPagersType.NumbersBehindWords) {
				
				CreatePreviousPager(this.PageIndex);
				CreateNextPager(this.PageIndex, pageSize);
				CreateNumericPagers(this.PageIndex, this.PageSize, this.MaxNumericPagers);
			}
			else if (type == RepeaterPagersType.NumbersBeforeWords) {
				
				CreateNumericPagers(this.PageIndex, this.PageSize, this.MaxNumericPagers);
				CreatePreviousPager(this.PageIndex);
				CreateNextPager(this.PageIndex, pageSize);
			}
		}


		/// <summary>
		/// Creates numeric pagers.
		/// </summary>
		/// <param name="currentIndex">Current page index</param>
		/// <param name="pageSize">Selected page size</param>
		/// <param name="maxPagers">Maximum amount of numeric pagers</param>
		protected virtual void			CreateNumericPagers(int currentIndex, int pageSize, int maxPagers)
		{
			int				totalPages	=	GetTotalPages(this.DataSourceSize, pageSize);

			if (totalPages <= 1)
				return;

			int				lastIndex	=	totalPages - 1;

			//	gets required pagers count.
			ArrayList	neededNumbers	=	new ArrayList();
			for(int i=0; i < totalPages; i++) {
				neededNumbers.Add(i);
			}

			//	list of avaliable pagers.
			ArrayList	shownPagers		=	new ArrayList();
			if (neededNumbers.Count > maxPagers) {

                int			lower		=	0;
				int			upper		=	0;

				int			hoodSize	=	(maxPagers - 1) / 2;
				int			half		=	this.DataSourceSize / 2;

				lower					=	currentIndex - hoodSize;
				upper					=	currentIndex + hoodSize;

				//	case when total amoung of pagers is even.
				if (maxPagers % 2 == 0) {

					if (currentIndex < half)
						upper++;
					else
						lower--;
				}

				while (lower < 0) {
				
					lower++;
					upper++;
				}

				while (upper > lastIndex) {
				
					lower--;
					upper--;
				}

				for(int i=lower; i <= upper; i++)
					shownPagers.Add(i);
			}
			else
				shownPagers				=	neededNumbers;

			//	text before.
			if (this.TextBeforeNumericPagers != null)
				Controls.Add(new LiteralControl(this.TextBeforeNumericPagers));

			//	first page pager.
			int		firstShownPager		=	(int)shownPagers[0];
			if (firstShownPager >= (this.EmptySpaceSize))
				this.CreatePager("pagerFirst", 0, this.NumericPagerTemplate);

			//	empty pager.
			if ((int)shownPagers[0] > 0) {
				
				int			preceding	=	(int)shownPagers[0] - 1;
				this.CreatePager("pager" + preceding.ToString(), preceding, this.EmptyPagerTemplate);
			}

			//	numeric pagers.
			foreach(int numeric in shownPagers) {
			
				this.CreatePager("pager" + numeric.ToString(), numeric,
					currentIndex.Equals(numeric) && SelectedNumericPagerTemplate != null ? this.SelectedNumericPagerTemplate : this.NumericPagerTemplate);
			}

			//	empty pager.
			int		lastShownPager		=	(int)shownPagers[shownPagers.Count-1];
			if (lastShownPager < lastIndex) {
				
				int			following	=	lastShownPager + 1;
				this.CreatePager("pager" + following.ToString(), following, this.EmptyPagerTemplate);
			}

			//	last page pager.
			if (lastShownPager <= (lastIndex - this.EmptySpaceSize))
				this.CreatePager("pagerLast", lastIndex, this.NumericPagerTemplate);

			//	text behind.
			if (this.TextBehindNumericPagers != null)
				Controls.Add(new LiteralControl(this.TextBehindNumericPagers));
		}


		/// <summary>
		/// Creates "previous" word pager.
		/// </summary>
		/// <param name="currentIndex">Current page index</param>
		protected virtual void			CreatePreviousPager(int currentIndex)
		{
			if (!ShouldUsePreviousPager(currentIndex))
				return;

			CreatePager("pagerPrevious", currentIndex - 1, this.PreviousPagerTemplate);
		}


		/// <summary>
		/// Checks if previous word pager is required.
		/// </summary>
		protected virtual bool			ShouldUsePreviousPager(int currentIndex)
		{
			return currentIndex > 0;
		}


		/// <summary>
		/// Creates "next" word pager.
		/// </summary>
		/// <param name="currentIndex">Current page index</param>
		/// <param name="pageSize">Page size</param>
		protected virtual void			CreateNextPager(int currentIndex, int pageSize)
		{
			if (!ShouldUseNextPager(currentIndex, pageSize))
				return;

			CreatePager("pagerNext", currentIndex + 1, this.NextPagerTemplate);
		}


		/// <summary>
		/// Checks if next word pager is required in current circumstances.
		/// </summary>
		protected virtual bool			ShouldUseNextPager(int currentIndex, int pageSize)
		{
			int					dsize	=	this.DataSourceSize;
			int				lastIndex	=	dsize / pageSize;
			if (dsize <= (lastIndex * pageSize))
				lastIndex--;

			return currentIndex < lastIndex;
		}


		/// <summary>
		/// Creates some pager.
		/// </summary>
		/// <param name="index">Index of page that pager points to</param>
		/// <param name="template">It's template</param>
		protected virtual void			CreatePager(string id, int index, ITemplate template)
		{
			if (template == null)
				return;

			RepeaterPagerItem	item	=	new RepeaterPagerItem(index);
			item.ID						=	id;
			template.InstantiateIn(item);
			Controls.Add(item);
		}


		/// <summary>
		/// Returns actual data that are provided to paged repeater.
		/// </summary>
		/// <param name="origData">Original data source</param>
		/// <param name="pageIndex">Current page index</param>
		/// <param name="pageSize">Page size</param>
		/// <param name="type">Paging type</param>
		protected virtual object		GetDataSource(ICollection origData, int pageIndex, int pageSize, RepeaterPagingType type)
		{
			if (origData == null)
				return null;

			if (type == RepeaterPagingType.Classic) {
			
				return GetPartOf(origData, pageIndex * pageSize, (pageIndex+1) * pageSize);
			}
			else if (type == RepeaterPagingType.VirtualItems) {
			
				return GetPartOf(origData, 0, this.VirtualItemsCount);
			}
			else
				return null;
		}


		/// <summary>
		/// Returns part of provided data source.
		/// </summary>
		/// <param name="coll">Input collection</param>
		/// <param name="from">Beginning index</param>
		/// <param name="to">Ending index</param>
		protected virtual ArrayList		GetPartOf(ICollection coll, int from, int to)
		{
			ArrayList			array	=	new ArrayList();

			int					helper	=	0;

			foreach(object item in coll) {
			
				if (helper >= from && helper < to)
					array.Add(item);

				helper++;
			}

			return array;
		}


		/// <summary>
		/// Returns total amount of pages that provided data source would require.
		/// </summary>
		/// <param name="sourceSize">Data source size</param>
		/// <param name="pageSize">Page size</param>
		protected virtual int			GetTotalPages(int sourceSize, int pageSize)
		{
			if (sourceSize < 0)
				throw new ArgumentException("Data source size must be non-negative.");

			if (pageSize <= 0)
				throw new ArgumentException("Page size must be greater than zero.");

			return sourceSize / pageSize + ((sourceSize % pageSize) > 0 ? 1 : 0);
		}
	}
}
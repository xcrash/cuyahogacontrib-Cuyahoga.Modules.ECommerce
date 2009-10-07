using System;
using System.Collections;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Util.Edi {

	/// <summary>
	/// Summary description for EdiOrderDocument.
	/// </summary>
	public class EdiDocument {

		public const string DEFAULT_DATA_ELEMENT_SEPARATOR = "|";
		public const string DEFAULT_SEGMENT_TERMINATOR = "\r\n";

		private string _dataElementSeparator = DEFAULT_DATA_ELEMENT_SEPARATOR;
		private string _segmentTerminator = DEFAULT_SEGMENT_TERMINATOR;

		private ArrayList _segments = new ArrayList();

		public EdiDocument() {
		}

		public string DataElementSeparator {
			get {
				return _dataElementSeparator;
			}
			set {
				_dataElementSeparator = value;
			}
		}

		public string SegmentTerminator {
			get {
				return _segmentTerminator;
			}
			set {
				_segmentTerminator = value;
			}
		}

		public ArrayList Segments {
			get {
				return _segments;
			}
		}

		public void AddSegment(EdiSegment segment) {
			_segments.Add(segment);
		}

		public override string ToString() {

			StringBuilder document = new StringBuilder();

			bool isFirstSegment = true;

			foreach (EdiSegment segment in Segments) {
				
				if (isFirstSegment == false) {
					document.Append(SegmentTerminator);
				} else {
					isFirstSegment = false;
				}

				document.Append(segment.ToString());
			}

			return document.ToString ();
		}

	}
}
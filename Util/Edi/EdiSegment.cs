using System;
using System.Collections;
using System.Text;

using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Util.Edi {

	public enum PaddingType : int {
		NoPadding = 1,
		PadLeft,
		PadRight
	}

	/// <summary>
	/// Summary description for EdiOrderLine.
	/// </summary>
	public class EdiSegment {

		private string _dataElementSeparator = "";
		private string _segmentIdentifier = "";

		private ArrayList _dataElements = new ArrayList();

		public EdiSegment(string identifier, string dataElementSeparator) {
			SegmentIdentifier = identifier;
			DataElementSeparator = dataElementSeparator;
		}

		public string SegmentIdentifier {
			get {
				return _segmentIdentifier;
			}
			set {
				_segmentIdentifier = value;
			}
		}

		public string DataElementSeparator {
			get {
				return _dataElementSeparator;
			}
			set {
				_dataElementSeparator = value;
			}
		}

		public ArrayList DataElements {
			get {
				return _dataElements;
			}
		}

		public void AddDataElement(string fieldValue, int length) {
			_dataElements.Add(StringUtils.Left(fieldValue, length));
		}
		
		public void AddDataElement(int fieldValue, int length) {
			_dataElements.Add(StringUtils.Left("" + fieldValue, length));
		}

		public void AddDataElement(double fieldValue, int length) {
			_dataElements.Add(StringUtils.Left("" + fieldValue, length));
		}

		public void AddDataElement(string fieldValue, int length, PaddingType paddingType, char paddingChar) {

			string newString = fieldValue;

			while (newString.Length < length && paddingType != PaddingType.NoPadding) {
				if (paddingType == PaddingType.PadLeft) {
					newString = paddingChar + newString;
				} else {
					newString = newString + paddingChar;
				}
			}

			_dataElements.Add(StringUtils.Left(newString, length));
		}

		public void AddDataElement(int fieldValue, int length, PaddingType paddingType, char paddingChar) {
			AddDataElement("" + fieldValue, length, paddingType, paddingChar);
		}

		public void AddDataElement(double fieldValue, int length, int numDecimalPlaces) {
			AddDataElement(fieldValue,length, numDecimalPlaces, PaddingType.NoPadding, ' ');
		}

		public void AddDataElement(double fieldValue, int length, int numDecimalPlaces, PaddingType paddingType, char paddingChar) {
			AddDataElement(fieldValue.ToString("F" + numDecimalPlaces), length, paddingType, paddingChar);
		}

		public override string ToString() {

			StringBuilder segment = new StringBuilder();
			
			segment.Append(SegmentIdentifier);

			foreach (string dataElement in DataElements) {
				segment.Append(DataElementSeparator + dataElement);
			}

			return segment.ToString ();
		}


	}
}
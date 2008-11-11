using System;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

	/// <summary>
	/// Summary description for SearchMetaData.
	/// </summary>
	public class SearchMetaData : ISearchMetaData {

        private string _searchTerm;
        private int _pageSize;
        private int _pageNumber;
        private int _resultCount;
        
        public SearchMetaData() {
		}

        public string SearchTerm {
            get { return _searchTerm; }
            set { _searchTerm = value; }
        }

        public int PageSize {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public int PageNumber {
            get { return _pageNumber; }
            set { _pageNumber = value; }
        }

        public int ResultCount {
            get { return _resultCount; }
            set { _resultCount = value; }
        }
	}
}
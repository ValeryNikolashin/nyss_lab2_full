using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NYSS_Lab2.Pagination
{
    public class PagingCollectionView : CollectionView
    {
        private readonly IList _innerList;
        private int itemsPerPage;

        public int ItemsPerPage {
            get { return itemsPerPage; }
            set { itemsPerPage = value; Refresh(); }
        }

        private int _currentPage = 1;

        public PagingCollectionView(IList innerList, int itemsPerPage)
            : base(innerList)
        {
            _innerList = innerList;
            ItemsPerPage = itemsPerPage;
        }

        public override int Count
        {
            get
            {
                if (_innerList.Count == 0) return 0;
                if (_currentPage < PageCount) // page 1..n-1
                {
                    return ItemsPerPage;
                }
                else // page n
                {
                    var itemsLeft = _innerList.Count % ItemsPerPage;
                    if (0 == itemsLeft)
                    {
                        return ItemsPerPage; // exactly itemsPerPage left
                    }
                    else
                    {
                        // return the remaining items
                        return itemsLeft;
                    }
                }
            }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentPage"));
            }
        }

        public int PageCount
        {
            get
            {
                return (_innerList.Count + ItemsPerPage - 1)
                    / ItemsPerPage;
            }
        }

        private int EndIndex
        {
            get
            {
                var end = _currentPage * ItemsPerPage - 1;
                return (end > _innerList.Count) ? _innerList.Count : end;
            }
        }

        private int StartIndex
        {
            get { return (_currentPage - 1) * ItemsPerPage; }
        }

        public override object GetItemAt(int index)
        {
            var offset = index % (ItemsPerPage);
            return _innerList[StartIndex + offset];
        }

        public void MoveToPage(int page)
        {
            if (page <= PageCount)
            {
                CurrentPage = page;
            }
            else CurrentPage = PageCount;
            Refresh();
        }

        public void MoveToNextPage()
        {
            if (_currentPage < PageCount)
            {
                CurrentPage += 1;
            }
            Refresh();
        }

        public void MoveToPreviousPage()
        {
            if (_currentPage > 1)
            {
                CurrentPage -= 1;
            }
            Refresh();
        }
    }
}

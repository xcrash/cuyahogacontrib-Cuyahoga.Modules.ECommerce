using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Core.Domain;
namespace Cuyahoga.Modules.ECommerce.Domain {
   public class CategoryLink {

       private string _imageUrl;
       private string _title;
       private Node _node;
       private Category _category;
       private long _categoryLinkID;
       private long _categoryID;
       private int _nodeID;

       public virtual int NodeID {
           get { return _nodeID; }
           set { _nodeID = value; }
       }

       public virtual long _CategoryLinkID {
           get { return _categoryLinkID; }
           set { _categoryLinkID = value; }
       }

       public virtual string ImageUrl {
           get { return _imageUrl; }
           set { _imageUrl = value; }
       }

       public virtual string Title {
           get { return _title; }
           set { _title = value; }
       }

       public virtual Category Category {
           get { return _category; }
           set { _category = value; }
       }

       public virtual Cuyahoga.Core.Domain.Node Node {
           get { return _node; }
           set { _node = value; }
       }

       public virtual long CategoryID {
           get { return _categoryID; }
           set { _categoryID = value; }
       }
    }
}

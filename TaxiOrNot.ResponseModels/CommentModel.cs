using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxiOrNot.ResponseModels
{
    public class CommentModel
    {
        public int Text { get; set; }

        public string User { get; set; }
    }

    public class NewCommentModel
    {

        public int Text { get; set; }
    }
}

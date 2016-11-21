namespace MJMail.Grid.Cells
{
    public class Cell
    {
        public Cell(string prefix, string postfix, string content, string type)
        {
            _prefix = prefix;
            _postfix = postfix;
            _content = content;
            _type = type;
        }

        string _ID { get; set; }
        string _prefix { get; set; }
        string _postfix { get; set; }
        string _content { get; set; }
        public string _type { get; set; }

        public string Build()
        {
            return _prefix + _content + _postfix;
        }

        #region GetSet
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }
        public string Prefix
        {
            get
            {
                return _prefix;
            }
            set
            {
                _prefix = value;
            }
        }
        #endregion
    }

}

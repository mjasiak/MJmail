namespace MJMail.Grid.Cells
{
    public class Cell
    {
        public Cell(string prefix, string postfix, string content)
        {
            _prefix = prefix;
            _postfix = postfix;
            _content = content;
        }

        string _ID { get; set; }
        string _prefix { get; set; }
        string _postfix { get; set; }
        string _content { get; set; }
        string _column { get; set; }

        public string Build()
        {
            return _prefix + _content + _postfix;
        }
    }

}

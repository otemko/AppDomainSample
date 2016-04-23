namespace ActionAlgorithm.ActionPlugins
{
    public interface IActionPlugin
    {
        TypeOperation Operation { get; set; }
        string Class { get; set; }
        string Assembly { get; set; }
    }
}
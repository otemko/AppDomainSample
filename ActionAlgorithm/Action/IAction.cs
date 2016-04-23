namespace ActionAlgorithm.Action
{
    public interface IAction
    {
        TypeOperation Operation { get; set; }
        double[] Data { get; set; }
    }
}
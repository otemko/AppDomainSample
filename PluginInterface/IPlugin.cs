namespace PluginInterface
{
    /// <summary>
    /// Represents a plugin that aggregates an array of double-precision numbers
    /// to a single double-precision number.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets the plugin's name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Performs this plugin's operation on specified array of double-precision
        /// number and returns the aggregation result.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        double Operation(double[] input);
    }
}

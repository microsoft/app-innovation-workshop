namespace ContosoFieldService.Abstractions
{
    /// <summary>
    /// Checks the current environment. Has be be implemented by each platform
    /// </summary>
    public interface IEnvironmentService
    {
        /// <summary>
        /// Checks if app is running on a real device by a user.
        /// </summary>
        /// <returns>
        ///     <c>false</c>, if running on a Simulator/Emulator, in Debug Mode or a in App Center Test Cloud, 
        ///     <c>true</c> otherwise.
        /// </returns>
        bool IsRunningInRealWorld();
    }
}

namespace MasterDesignPattern.Singleton
{
    internal class EagerSingleton
    {
        public void Simulate()
        {
            var appSetting = AppSetting.Instance;
            appSetting.ApplySetting("Dark", "Medium");  
        }
    }

    public interface IAppSetting
    {
        void ApplySetting(string theme, string fontSize);
    }

    public class AppSetting : IAppSetting
    {
        private static readonly AppSetting _instance;
        // Private constructor to prevent external instantiation
        private AppSetting() { }

        static AppSetting()
        {
            _instance = new AppSetting();
        }

        public static AppSetting Instance => _instance;

        public void ApplySetting(string theme, string fontSize)
        {
            Console.WriteLine($"Applied Theme: {theme}, Font Size: {fontSize}");
        }
    }
}

namespace MasterDesignPattern.Prototype
{
    internal class RestoreSettingDefault
    {
        public void Simulate()
        {
            var defaultSetting = DefaultSetting.SetDefaultSetting();
            Console.WriteLine($"Custom Setting {defaultSetting.FontName} {defaultSetting.ThemeName} {defaultSetting.FontSize}");
            //User change the setting

            var userSetting = defaultSetting.Clone();
            userSetting.FontName = "Arial";
            userSetting.ThemeName = "Dark";

            Console.WriteLine($"Custom Setting {userSetting.FontName} {userSetting.ThemeName} {userSetting.FontSize}");

            //Restore to default

            userSetting.Restore(defaultSetting);
            Console.WriteLine($"Restore Setting {userSetting.FontName} {userSetting.ThemeName} {userSetting.FontSize}");

        }
    }

    /*
     When application start 
     -> start with default setting 
     -> then user made some change in default setting
     -> now user want to restore to default setting     
     */

    //Polymorphism
    //Loss coupling clone logic from client
    public class AppSetting : IPrototype<AppSetting>
    {
        public int FontSize { get; set; }
        public string ThemeName { get; set; }
        public string FontName { get; set; }

        //Encapsulate  logic of clone
        //Hide implemention details
        public AppSetting Clone()
        {
            return (AppSetting)MemberwiseClone();
        }

        public void Restore(AppSetting source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            FontSize = source.FontSize;
            ThemeName = source.ThemeName;
            FontName = source.FontName;
        }
    }

    public class DefaultSetting
    {
        public static AppSetting SetDefaultSetting() =>
            new AppSetting { FontSize = 10, FontName = "Console", ThemeName = "Default" };
    }
}

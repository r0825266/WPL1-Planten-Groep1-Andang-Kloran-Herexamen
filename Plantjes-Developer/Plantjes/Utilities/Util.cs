using System.Diagnostics;
using System.Windows;
using Newtonsoft.Json;

namespace Plantjes; 

public class Util {
    
}

public static class UtilExtensions {
    //Instellingen voor object-representatie
    public static JsonSerializerSettings jss = new JsonSerializerSettings() {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        Formatting = Formatting.Indented
    };
    //print object naar console, handig als je honderden objecten wil zien
    public static void PrintDebug(this object myObj) {
        Debug.WriteLine(JsonConvert.SerializeObject(myObj, jss));
    }
    //boolean naar visibility
    public static Visibility AsVisibility(this bool visibility) {
        return visibility ? Visibility.Visible : Visibility.Collapsed;
    }
}
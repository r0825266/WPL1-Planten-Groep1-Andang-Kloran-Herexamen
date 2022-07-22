using Plantjes.Models.Db;
//using Plantjes.DAL.Models;

/*comments kenny*/
//using System.Windows.Controls;

namespace Plantjes.Dao;

public class DAOLogic
{
    //Robin: opzetten DAOLogic, singleton pattern

    //1.een statische private instantie instatieren die enkel kan gelezen worden.
    //deze wordt altijd teruggegeven wanneer de Instance method wordt opgeroepen
    private static DAOLogic instance = new();

    /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/

    //Xander - move ctor call to object declaration
    public static plantenContext context = new();

    //3.publieke methode instance die altijd kan aangeroepen worden
    //door zijn statische eigenschappen kan hij altijd aangeroepen worden 
    //zonder er een instantie van te maken
    public static DAOLogic Instance()
    {
        return instance;
    }
}
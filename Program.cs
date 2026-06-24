using classlibrary_console_web_proj1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using webapplication_console_web_proj1;
using webapplication_console_web_proj1.Models;
//using System.Net.Http;


namespace console_web_proj1
{
    internal class Program
    {
        //private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            ClassClientWebApi classClientWebApi = new ClassClientWebApi();
            int userInput = 0;
            do
            {
                System.Console.WriteLine("shoe inventory application.");
                System.Console.WriteLine("1 - to get all shoe items.");
                System.Console.WriteLine("2 - to get shooe item  by id.");
                System.Console.WriteLine("3 - to add new shoe item.");
                System.Console.WriteLine("4 - to edit shoe item.");
                System.Console.WriteLine("5 - to delete shoe item by id.");
                System.Console.WriteLine("-1 - exit program");
                System.Console.Write("user input: ");
                userInput = Convert.ToInt32(System.Console.ReadLine().Trim());
                switch (userInput)
                {
                    case 1:
                        //get all shoe items
                        Task taskFnGetAllShoeItems = Task.Run(() => classClientWebApi.FnGetAllShoeItems());
                        await taskFnGetAllShoeItems;
                        break;
                    case 2:
                        //get shoe item by id
                        int userInputShoeId;
                        System.Console.Write("enter shoe id: ");
                        string userInputShoeIdStr = System.Console.ReadLine().Trim();
                        if (int.TryParse(userInputShoeIdStr, out userInputShoeId))
                        {
                            Task taskFnGetShoeInventoryItemId = Task.Run(() => classClientWebApi.FnGetShoeInventoryItemId(userInputShoeId));
                            await taskFnGetShoeInventoryItemId;
                        }
                        else
                        {
                            System.Console.WriteLine("value entered for shoe id not valid.");
                        }
                        break;
                    case 3:
                        //add new shoe item
                        System.Console.Write("shoe name: ");
                        string shoeName = System.Console.ReadLine().Trim();
                        if (shoeName == string.Empty) { shoeName = "n/a"; }
                        System.Console.Write("shoe description: ");
                        string shoeDescription = System.Console.ReadLine().Trim();
                        if (shoeDescription == string.Empty) { shoeDescription = "n/a"; }
                        System.Console.Write("shoe price: ");
                        string shoePrice = System.Console.ReadLine().Trim();
                        if (shoePrice == string.Empty) { shoePrice = "n/a"; }
                        System.Console.Write("shoe quantity: ");
                        int shoeQuantity;
                        string shoeQuantityStr = System.Console.ReadLine().Trim();
                        if (!int.TryParse(shoeQuantityStr, out shoeQuantity))
                        {
                            shoeQuantity = 0;
                        }
                        PostShoeInventoryTableModel postShoeInventoryTableModel = new PostShoeInventoryTableModel();
                        postShoeInventoryTableModel.shoeName = shoeName;
                        postShoeInventoryTableModel.shoeDescription = shoeDescription;
                        postShoeInventoryTableModel.shoePrice = shoePrice;
                        postShoeInventoryTableModel.quantity = shoeQuantity;
                        Task taskFnAddNewShoeInventoryItem = Task.Run(() => classClientWebApi.FnAddNewShoeInventoryItem(postShoeInventoryTableModel));
                        await taskFnAddNewShoeInventoryItem;
                        break;
                    case 4:
                        //edit shoe item
                        System.Console.WriteLine("enter details of shoe item including id for edit.");
                        System.Console.Write("shoe id: ");
                        string shoeIdEditStr = System.Console.ReadLine().Trim();
                        int shoeIdEdit;
                        if (!int.TryParse(shoeIdEditStr, out shoeIdEdit))
                        {
                            System.Console.WriteLine("shoe id entered not valid.");
                            break;
                        }
                        System.Console.Write("shoe name: ");
                        string shoeNameEdit = System.Console.ReadLine().Trim();
                        if (shoeNameEdit == string.Empty) { shoeNameEdit = "n/a"; }
                        System.Console.Write("shoe description: ");
                        string shoeDescriptionEdit = System.Console.ReadLine().Trim();
                        if (shoeDescriptionEdit == string.Empty) { shoeDescriptionEdit = "n/a"; }
                        System.Console.Write("shoe price: ");
                        string shoePriceEdit = System.Console.ReadLine().Trim();
                        if (shoePriceEdit == string.Empty) { shoePriceEdit = "n/a"; }
                        System.Console.Write("shoe quantity: ");
                        int shoeQuantityEdit;
                        string shoeQuantityEditStr = System.Console.ReadLine().Trim();
                        if (!int.TryParse(shoeQuantityEditStr, out shoeQuantityEdit))
                        {
                            System.Console.WriteLine("shoe quantityd entered not valid.");
                            break;
                        }
                        ShoeInventoryTableModel shoeInventoryTableModel = new ShoeInventoryTableModel();
                        shoeInventoryTableModel.id = shoeIdEdit;
                        shoeInventoryTableModel.shoeName = shoeNameEdit;
                        shoeInventoryTableModel.shoeDescription = shoeDescriptionEdit;
                        shoeInventoryTableModel.shoePrice = shoePriceEdit;
                        shoeInventoryTableModel.quantity = shoeQuantityEdit;
                        Task taskFnUpdateShoeInventoryItemDetail = Task.Run(() =>
                            classClientWebApi.FnUpdateShoeInventoryItemDetail(shoeInventoryTableModel));
                        await taskFnUpdateShoeInventoryItemDetail;
                        break;
                    case 5:
                        //delete shoe item by id
                        int shoeIdDelete;
                        System.Console.Write("enter shoe id: ");
                        string shoeIdDeleteStr = System.Console.ReadLine().Trim();
                        if (int.TryParse(shoeIdDeleteStr, out shoeIdDelete))
                        {
                            Task taskFnDeleteShoeInventoryItemId = Task.Run(() =>
                                classClientWebApi.FnDeleteShoeInventoryItemId(shoeIdDelete));
                            await taskFnDeleteShoeInventoryItemId;
                        }
                        else
                        {
                            System.Console.WriteLine("value entered for shoe id not valid.");
                        }
                        break;
                    case -1:
                        System.Console.WriteLine("program end.");
                        break;
                    default:
                        System.Console.WriteLine("user input not valid.");
                        break;
                }
            } while (userInput != -1);

        }
    }//end-class
}//end-namespace


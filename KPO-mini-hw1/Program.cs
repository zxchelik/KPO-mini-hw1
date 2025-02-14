using Microsoft.Extensions.DependencyInjection;
using Core;
using Core.Inventory;
using Core.Models;

namespace KPO_mini_hw1;

internal abstract class Program
{
    private static void Main()
    {
        var menu = new Menu();
        menu.Run();
    }
}
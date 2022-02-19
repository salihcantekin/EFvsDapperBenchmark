using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public static class Constants
    {
        //public static String ConnectionStringDapper { get; } = "User ID=pgloader_user;password=pgloader_user;Host=localhost;Port=5432;Database=general;SearchPath=efdapperbenchmark";
        //public static String ConnectionStringEF { get; } = "User ID=pgloader_user;password=pgloader_user;Host=localhost;Port=5432;Database=general;SearchPath=efdapperbenchmark";
        public static String ConnectionStringDapper { get; } = "Server=localhost;Database=efdapperbenchmark;User Id=sa;Password=Salih123!;";
        public static String ConnectionStringEF { get; } = "Server=localhost;Database=efdapperbenchmark;User Id=sa;Password=Salih123!;";
    }
}

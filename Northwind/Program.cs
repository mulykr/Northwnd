namespace Northwnd
{
    class Program
    {
        static void Main(string[] args)
        {
            NorthwndQueries queries = new NorthwndQueries();
            NorthwndConsoleUi ui = new NorthwndConsoleUi();
            ui.Add(queries.Q1, "Show all info about the employee with ID 8.");
            ui.Add(queries.Q2, "Show the list of first and last names of the employees from London.");
            ui.Add(queries.Q3, "Show the list of first and last names of the employees whose first name begins with letter A.");

            ui.Launch();
        }
    }
}

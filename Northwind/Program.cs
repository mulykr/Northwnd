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
            ui.Add(queries.Q5, "Calculate the count of employees from London.");
            ui.Add(queries.Q9, "Show the first and last name(s) of the eldest employee(s).");
            ui.Add(queries.Q10, "Show first, last names and ages of 3 eldest employees.");
            ui.Add(queries.Q11, "Show the list of all cities where the employees are from");
            ui.Add(queries.Q13, "Show first and last names of the employees who used to serve orders shipped to Madrid.");

            ui.Launch();
        }
    }
}

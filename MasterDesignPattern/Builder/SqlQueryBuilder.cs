namespace MasterDesignPattern.Builder
{
    internal class SqlQueryBuilder
    {
        public void CreateQuery()
        {
            Console.WriteLine("-----------------Sql Query Builder------------");
            SqlQuery sqlQuery = SqlQueryBuilderStep.CreateBuilder()
                 .Select("SELECT *")
                 .From("FROM Users")
                 .Where("WHERE Age > 18")
                 .OrderBy("ORDER BY Name")
                 .Join("JOIN Orders ON Users.Id = Orders.UserId")
                 .Build();
            sqlQuery.Execute();
        }
    }

    /*
     Build a sql query builder where we have run the query in 
       -Select -Mandatory
       -From   -Mandtory
       -Join   -Optional
       -Where  -Optional
       -OrderBy -Optional


    Sequence is also matter first select then from then optional
     */

    // Target and Immutable Object
    public class SqlQuery
    {
        public string Select { get; }

        public string From { get; }

        public string Join { get; }

        public string Where { get; }

        public string OrderBy { get; }

        public SqlQuery(string select, string from, string join, string where, string orderBy)
        {
            Select = select;
            From = from;
            Join = join;
            Where = where;
            OrderBy = orderBy;
        }

        public void Execute()
        {
            Console.WriteLine($"Executing SQL Query: {Select} {From}");
            if (!string.IsNullOrEmpty(Join))
            {
                Console.WriteLine($"Join: {Join}");
            }
            if (!string.IsNullOrEmpty(Where))
            {
                Console.WriteLine($"Where: {Where}");
            }

            if (!string.IsNullOrEmpty(OrderBy))
            {
                Console.WriteLine($"Order: {OrderBy}");
            }
            // Here you would add the actual database execution logic
        }
    }

    public interface ISelectStep
    {
        IFromStep Select(string select);
    }

    public interface IFromStep
    {
        IClauseStep From(string tablename);
    }

    public interface IClauseStep
    {
        IClauseStep Where(string where);
        IClauseStep OrderBy(string orderBy);
        IClauseStep Join(string join);

        SqlQuery Build();
    }

    public class SqlQueryBuilderStep : ISelectStep, IFromStep, IClauseStep
    {
        private string _select;
        private string _from;
        private string _join;
        private string _where;
        private string _orderBy;

        //Benefits of private constructor not allow to create the object directly
        private SqlQueryBuilderStep() { }

        //This step will help to create the builder object and start the building process
        public static ISelectStep CreateBuilder()
        {
            return new SqlQueryBuilderStep();
        }

        public IFromStep Select(string select)
        {
            _select = select;
            return this;
        }

        public IClauseStep From(string tablename)
        {
            _from = tablename;
            return this;
        }
        public IClauseStep Where(string where)
        {
            _where = where;
            return this;
        }
        public IClauseStep OrderBy(string orderBy)
        {
            _orderBy = orderBy;
            return this;
        }
        public IClauseStep Join(string join)
        {
            _join = join;
            return this;
        }
        public SqlQuery Build()
        {
            // Validate mandatory fields
            if (string.IsNullOrEmpty(_select))
                throw new InvalidOperationException("SELECT clause is mandatory.");
            if (string.IsNullOrEmpty(_from))
                throw new InvalidOperationException("FROM clause is mandatory.");
            return new SqlQuery(_select, _from, _join, _where, _orderBy);
        }
    }
}

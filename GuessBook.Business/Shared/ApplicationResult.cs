namespace GuessBook.Business.Shared
{
    public class ApplicationResult<T> : BaseApplicationResult
    {
        public T ValueResult { get; set; }
    }
    public class ApplicationResult : BaseApplicationResult
    {

    }
    public class BaseApplicationResult
    {
      public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

    }
}

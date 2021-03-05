namespace RentACar.Core.Utilities.Results
{
    public class Result : IResult
    {
        public string Message { get; }

        public bool IsSuccees { get; }

        public Result(bool isSuccess)
        {
            IsSuccees = isSuccess;
        }

        public Result(string message, bool isSuccess) : this(isSuccess)
        {
            Message = message;
        }
    }
}

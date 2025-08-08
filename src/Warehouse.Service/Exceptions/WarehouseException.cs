namespace Warehouse.Service.Exceptions;

public class WarehouseException : Exception
{
    public int Code { get; set; }

    public WarehouseException(int code = 500, string message = "Something went wrong") : base(message)
    {
        this.Code = code;
    }
}
namespace OrderFlow.Domain.Orders;

public enum OrderStatus
{
    Draft = 1,
    PendingValidation = 2,
    Validated = 3,
    ReservationPending = 4,
    Reserved = 5,
    Failed = 6,
    Cancelled = 7
}
namespace Domain.Enums
{
    public enum ActionState
    {
        Pay = 0,
        Finish = 1, //pay and use
        Cancel= 2, //can never be paid
        Refound = 3, //if has paid
        Reopen = 4, //just canceled
    }
}

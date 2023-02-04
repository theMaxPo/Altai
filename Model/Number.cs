public class Number
{
    public int Value { get; private set; }
    public Number(int value)
    {
        Value = value;
    }

    public Number AddedTo(Number other)
    {
        return new Number(Value + other.Value);
    }

    public Number SubbedBy(Number other)
    {
        return new Number(Value - other.Value);
    }

    public Number MultedBy(Number other)
    {
        return new Number(Value * other.Value);
    }

    public Number DivedBy(Number other)
    {
        return new Number(Value / other.Value);
    }

    public override string ToString() => Value.ToString();
}
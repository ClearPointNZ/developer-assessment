using AutoFixture.Xunit2;

namespace TodoList.Testing;

public class InlineServiceInjectionAttribute : InlineAutoDataAttribute
{
    public InlineServiceInjectionAttribute(params object[] values)
        : base(new ServiceInjectionAttribute(), values)
    { }
}

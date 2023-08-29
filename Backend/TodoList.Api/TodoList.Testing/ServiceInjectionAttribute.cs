using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using TodoList.Testing.Generators;

namespace TodoList.Testing;

public class ServiceInjectionAttribute : AutoDataAttribute
{
    public ServiceInjectionAttribute() : base(Build)
    {

    }

    public static IFixture Build()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());

        // Generator
        fixture.Register(TodoItemGenerator.Create);

        return fixture;
    }
}

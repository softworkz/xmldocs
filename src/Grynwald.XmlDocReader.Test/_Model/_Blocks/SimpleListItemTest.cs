﻿namespace Grynwald.XmlDocReader.Test;

/// <summary>
/// Tests for <see cref="SimpleListItem"/>
/// </summary>
public class SimpleListItemTest
{
    [Fact]
    public void Text_must_not_be_null()
    {
        // ARRANGE

        // ACT 
        var ex = Record.Exception(() => new SimpleListItem(text: null!));

        // ASSERT
        var argumentNullException = Assert.IsType<ArgumentNullException>(ex);
        Assert.Equal("text", argumentNullException.ParamName);
    }

    [Fact]
    public void Two_instances_are_equal_if_their_text_is_equal()
    {
        // ARRANGE
        var instance1 = new SimpleListItem(new TextBlock(new PlainTextElement("Some text")));
        var instance2 = new SimpleListItem(new TextBlock(new PlainTextElement("Some text")));

        // ACT / ASSERT
        Assert.Equal(instance1.GetHashCode(), instance2.GetHashCode());

        Assert.True(instance1.Equals((object)instance2));
        Assert.True(instance2.Equals((object)instance1));

        Assert.True(instance1.Equals(instance2));
        Assert.True(instance2.Equals(instance1));
    }

    [Fact]
    public void Two_instances_are_not_equal_if_their_text_is_not_equal()
    {
        // ARRANGE
        var instance1 = new SimpleListItem(new TextBlock(new PlainTextElement("Some text")));
        var instance2 = new SimpleListItem(new TextBlock(new PlainTextElement("Some other text")));

        // ACT / ASSERT
        Assert.False(instance1.Equals((object)instance2));
        Assert.False(instance2.Equals((object)instance1));

        Assert.False(instance1.Equals(instance2));
        Assert.False(instance2.Equals(instance1));
    }

    [Fact]
    public void Equals_returns_false_when_comparing_to_null()
    {
        // ARRANGE
        var sut = new SimpleListItem(new TextBlock(new PlainTextElement("Some text")));

        // ACT / ASSERT
        Assert.False(sut.Equals((object?)null));
        Assert.False(sut!.Equals(null));
    }
}

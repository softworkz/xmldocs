﻿namespace Grynwald.XmlDocReader;

/// <summary>
/// Represents a <c><![CDATA[<item>]]></c> or <c><![CDATA[<listheader>]]></c> element inside a <![CDATA[<list />]]> element in XML documentation comments.
/// </summary>
/// <seealso cref="ListElement"/>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/recommended-tags">Recommended XML tags for C# documentation comments (Microsoft Learn)</seealso>
public sealed class ListItemElement : TextElement, IEquatable<ListItemElement>
{
    /// <summary>
    /// The term described by the list item.
    /// </summary>
    /// <value>
    /// The content of the <c>term></c> element if it was specified or <c>null</c>
    /// </value>
    public TextBlock? Term { get; }

    /// <summary>
    /// Gets the list item's content.
    /// </summary>
    public TextBlock Description { get; }


    /// <summary>
    /// Initializes a new instance of <see cref="ListItemElement"/>
    /// </summary>
    /// <param name="term">The content of the list items <c>term</c> element. Can be <c>null</c></param>
    /// <param name="description">The content of the list items <c>description</c> element.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="description"/> is <c>null</c>.</exception>
    public ListItemElement(TextBlock? term, TextBlock description)
    {
        Term = term;
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }


    /// <inheritdoc />
    public override void Accept(IDocumentationVisitor visitor) => visitor.Visit(this);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Description, Term);

    /// <inheritdoc />                                                                  
    public override bool Equals(object? obj) => Equals(obj as ListItemElement);

    /// <inheritdoc />
    public bool Equals(ListItemElement? other)
    {
        if (other is null)
            return false;


        if (Term is not null)
        {
            if (!Term.Equals(other.Term))
                return false;
        }
        else
        {
            if (other.Term is not null)
                return false;
        }

        return Description.Equals(other.Description);
    }


    /// <inheritdoc cref="FromXml(XElement)"/>
    public static ListItemElement FromXml(string xml) => FromXml(XmlContentHelper.ParseXmlElement(xml));

    /// <summary>
    /// Initializes a new <see cref="ListItemElement" /> from it's XML equivalent.
    /// </summary>
    public static ListItemElement FromXml(XElement xml)
    {
        xml.EnsureNameIs("listheader", "item"); //TODO: Consider using separate types for items/header

        //TextBlock? description = null;

        var term = xml.Element("term") is XElement termElement
            ? TextBlock.FromXml(termElement)
            : null;

        var description = xml.Element("description") is XElement descriptionElement
            ? TextBlock.FromXml(descriptionElement)
            : new TextBlock();  //TODO: Remove empty and use null instead

        //TODO: Warn on unrecognized elements
        //TODO: Warn if there are multiple term/description elements
        //TODO: Should description really be optional???

        return new ListItemElement(term, description);
    }
}

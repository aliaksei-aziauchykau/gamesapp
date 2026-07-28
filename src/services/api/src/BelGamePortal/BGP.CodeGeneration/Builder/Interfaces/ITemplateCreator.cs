using System;

namespace BGP.CodeGeneration.Builder.Interfaces;

public interface ITemplateCreator
{
    string Create(string template, object model);
}

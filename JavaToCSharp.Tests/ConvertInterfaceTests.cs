using System;
using Xunit;

namespace JavaToCSharp.Tests
{
    public class ConvertInterfaceTests
    {
        [Fact]
        public void Verify_Method_Param_Types_Use_New_Interface_Name_Types()
        {
            var javaCode = @"import com.github.javaparser.resolution.Context;
import com.github.javaparser.resolution.Test;
public interface ResolvedType {
    default boolean isArray() {
        return false;
    }
}

public class InferenceVariableType implements ResolvedType {
    public void registerEquivalentType(ResolvedType type) {
    }
}

public interface ResolvedValueDeclaration extends ResolvedDeclaration {
    ResolvedType getType();
}
";
            var options = new JavaConversionOptions { StartInterfaceNamesWithI = true };
            options.WarningEncountered += (_, eventArgs)
                                              => Console.WriteLine("Line {0}: {1}", eventArgs.JavaLineNumber, eventArgs.Message);
            var parsed = JavaToCSharpConverter.ConvertText(javaCode, options);

            var expectedCSharpCode = @"using Com.Github.Javaparser.Resolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp
{
    public interface IResolvedType
    {
        bool IsArray();
    }

    public class InferenceVariableType : IResolvedType
    {
        public virtual void RegisterEquivalentType(IResolvedType type)
        {
        }
    }

    public interface IResolvedValueDeclaration
    {
        IResolvedType GetType();
    }
}"; 
            
            Assert.Equal(expectedCSharpCode, parsed);
        }

    }
}
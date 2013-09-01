using Machine.Specifications;
using Praetorium.Collections;
using Praetorium.Contexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Praetorium.UnitTests.Utilities
{
    class IsOrDerivesFromSpecs : TypeExtensionSpecs
    {
        It true_when_comparing_closed_interface_to_open_interface = () =>
            typeof(IEnumerable<string>).IsOrDerivesFrom(typeof(IEnumerable<>)).ShouldBeTrue();

        It true_when_comparing_closed_interface_to_base_interface = () =>
            typeof(IEnumerable<string>).IsOrDerivesFrom(typeof(IEnumerable)).ShouldBeTrue();

        It true_when_comparing_open_to_same_open = () =>
            typeof(IEnumerable<>).IsOrDerivesFrom(typeof(IEnumerable<>)).ShouldBeTrue();

        It true_when_comparing_open_interface_to_base_interface = () =>
            typeof(IEnumerable<>).IsOrDerivesFrom(typeof(IEnumerable)).ShouldBeTrue();

        It true_when_comparing_derived_class_to_base_class = () =>
            typeof(string).IsOrDerivesFrom(typeof(object)).ShouldBeTrue();

        It true_when_comparing_class_to_itself = () =>
            typeof(string).IsOrDerivesFrom(typeof(string)).ShouldBeTrue();

        It true_when_comparing_closed_class_to_open_class = () =>
            typeof(Dictionary<string, string>).IsOrDerivesFrom(typeof(Dictionary<,>)).ShouldBeTrue();

        It true_when_comparing_closed_class_to_base_interface = () =>
            typeof(Dictionary<string, string>).IsOrDerivesFrom(typeof(IEnumerable)).ShouldBeTrue();

        It true_when_comparing_closed_class_to_base_open_interface = () =>
            typeof(List<string>).IsOrDerivesFrom(typeof(IEnumerable<>)).ShouldBeTrue();

        It true_when_comparing_multi_closed_class_to_multi_base_open_interface = () =>
            typeof(Dictionary<string, string>).IsOrDerivesFrom(typeof(IDictionary<,>)).ShouldBeTrue();

        It true_when_comparing_class_to_base_interface = () =>
            typeof(DictionaryContext).IsOrDerivesFrom(typeof(IContext)).ShouldBeTrue();

        It true_when_comparing_class_to_base_closed_interface = () =>
            typeof(string).IsOrDerivesFrom(typeof(IEnumerable<char>)).ShouldBeTrue();

        It true_when_comparing_class_to_base_open_interface = () =>
            typeof(string).IsOrDerivesFrom(typeof(IEnumerable<>)).ShouldBeTrue();

        It true_when_comparing_open_class_to_base_open_interface = () =>
            typeof(Dictionary<,>).IsOrDerivesFrom(typeof(IDictionary<,>)).ShouldBeTrue();

        It true_when_comparing_open_class_to_base_open_class = () =>
            typeof(FunctionKeyedCollection<,>).IsOrDerivesFrom(typeof(KeyedCollection<,>)).ShouldBeTrue();

        It true_when_comparing_struct_to_base_interface = () =>
            typeof(int).DerivesFrom(typeof(IConvertible)).ShouldBeTrue();

        It true_when_comparing_struct_to_base_open_interface = () =>
            typeof(int).DerivesFrom(typeof(IEquatable<>)).ShouldBeTrue();

        It true_when_comparing_struct_to_base_closed_interface = () =>
            typeof(int).DerivesFrom(typeof(IEquatable<int>)).ShouldBeTrue();

        It false_when_comparing_struct_to_wrong_closed_interface = () =>
            typeof(int).DerivesFrom(typeof(IEquatable<string>)).ShouldBeFalse();

        It false_when_comparing_type_to_different_type = () =>
            typeof(int).IsOrDerivesFrom<string>().ShouldBeFalse();

        It true_when_comparing_closed_class_to_itself = () =>
            typeof(Dictionary<string, string>).IsOrDerivesFrom(typeof(Dictionary<string, string>)).ShouldBeTrue();

        It false_when_comparing_null_to_type = () =>
            NullType.IsOrDerivesFrom(typeof(string)).ShouldBeFalse();

        It false_when_comparing_type_to_null = () =>
            typeof(string).IsOrDerivesFrom(null).ShouldBeFalse();

        It false_when_comparing_null_to_null = () =>
            NullType.IsOrDerivesFrom(null).ShouldBeFalse();
    }

    class DerivesFromSpecs : TypeExtensionSpecs
    {
        It true_when_comparing_closed_interface_to_open_interface = () =>
            typeof(IEnumerable<string>).DerivesFrom(typeof(IEnumerable<>)).ShouldBeTrue();

        It true_when_comparing_closed_interface_to_base_interface = () =>
            typeof(IEnumerable<string>).DerivesFrom(typeof(IEnumerable)).ShouldBeTrue();

        It false_when_comparing_open_to_same_open = () =>
            typeof(IEnumerable<>).DerivesFrom(typeof(IEnumerable<>)).ShouldBeFalse();

        It true_when_comparing_open_interface_to_base_interface = () =>
            typeof(IEnumerable<>).DerivesFrom(typeof(IEnumerable)).ShouldBeTrue();

        It true_when_comparing_derived_class_to_base_class = () =>
            typeof(string).DerivesFrom(typeof(object)).ShouldBeTrue();

        It false_when_comparing_class_to_itself = () =>
            typeof(string).DerivesFrom(typeof(string)).ShouldBeFalse();

        It true_when_comparing_closed_class_to_open_class = () => 
            typeof(Dictionary<string, string>).DerivesFrom(typeof(Dictionary<,>)).ShouldBeTrue();

        It true_when_comparing_closed_class_to_base_interface = () =>
            typeof(Dictionary<string, string>).DerivesFrom(typeof(IEnumerable)).ShouldBeTrue();
            
        It true_when_comparing_closed_class_to_base_open_interface = () =>
            typeof(List<string>).DerivesFrom(typeof(IEnumerable<>)).ShouldBeTrue();

        It true_when_comparing_multi_closed_class_to_multi_base_open_interface = () => 
            typeof(Dictionary<string, string>).DerivesFrom(typeof(IDictionary<,>)).ShouldBeTrue();

        It true_when_comparing_class_to_base_interface = () =>
            typeof(DictionaryContext).DerivesFrom(typeof(IContext)).ShouldBeTrue();

        It true_when_comparing_class_to_base_closed_interface = () =>
            typeof(string).DerivesFrom(typeof(IEnumerable<char>)).ShouldBeTrue();

        It true_when_comparing_class_to_base_open_interface = () =>
            typeof(string).DerivesFrom(typeof(IEnumerable<>)).ShouldBeTrue();

        It true_when_comparing_open_class_to_base_open_interface = () =>
            typeof(Dictionary<,>).DerivesFrom(typeof(IDictionary<,>)).ShouldBeTrue();

        It true_when_comparing_open_class_to_base_open_class = () =>
            typeof(FunctionKeyedCollection<,>).DerivesFrom(typeof(KeyedCollection<,>)).ShouldBeTrue();

        It true_when_comparing_struct_to_base_interface = () =>
            typeof(int).DerivesFrom(typeof(IConvertible)).ShouldBeTrue();

        It true_when_comparing_struct_to_base_open_interface = () =>
            typeof(int).DerivesFrom(typeof(IEquatable<>)).ShouldBeTrue();

        It true_when_comparing_struct_to_base_closed_interface = () =>
            typeof(int).DerivesFrom(typeof(IEquatable<int>)).ShouldBeTrue();

        It false_when_comparing_struct_to_wrong_closed_interface = () =>
            typeof(int).DerivesFrom(typeof(IEquatable<string>)).ShouldBeFalse();

        It false_when_comparing_type_to_different_type = () =>
            typeof(int).DerivesFrom<string>().ShouldBeFalse();
        
        It false_when_comparing_closed_class_to_itself = () =>
            typeof(Dictionary<string, string>).DerivesFrom(typeof(Dictionary<string, string>)).ShouldBeFalse();

        It false_when_comparing_null_to_type = () =>
            NullType.DerivesFrom(typeof(string)).ShouldBeFalse();

        It false_when_comparing_type_to_null = () =>
            typeof(string).DerivesFrom(null).ShouldBeFalse();

        It false_when_comparing_null_to_null = () =>
            NullType.DerivesFrom(null).ShouldBeFalse();
    }
}

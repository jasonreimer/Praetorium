using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Praetorium.UnitTests.Utilities
{
    class ClosesSpecs : TypeExtensionSpecs
    {
        It true_when_comparing_closed_class_to_open_interface = () =>
            typeof(string).Closes(typeof(IEnumerable<>)).ShouldBeTrue();

        It false_when_comparing_open_class_to_open_interface = () =>
            typeof(KeyedCollection<,>).Closes(typeof(IEnumerable<>)).ShouldBeFalse();

        It true_when_comparing_open_closed_interface_to_open_interface = () =>
            typeof(IEnumerable<string>).Closes(typeof(IEnumerable<>)).ShouldBeTrue();

        It false_when_comparing_class_to_closed_interface = () =>
            typeof(string).Closes(typeof(IEnumerable<char>)).ShouldBeFalse();

        It false_when_comparing_null_type_to_type = () =>
            NullType.Closes(typeof(IEnumerable<>)).ShouldBeFalse();

        It false_when_comparing_type_to_null = () =>
            typeof(IEnumerable<string>).Closes(null).ShouldBeFalse();

        It false_when_comparing_null_to_null = () =>
            NullType.Closes(null).ShouldBeFalse();
    }
}

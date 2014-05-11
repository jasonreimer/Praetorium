using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Praetorium.UnitTests.Caching
{
    public class TypeCacheSpecs
    {
        private static readonly Type _type = typeof(TestType);
        private readonly ITypeCache _cache = new TypeCache();

        [Fact]
        public void Cache_should_return_public_readonly_props()
        {
            _cache.GetPublicReadonlyInstanceProperties(_type).Should().HaveCount(1);
        }

        [Fact]
        public void Cache_should_return_public_props()
        {
            _cache.GetPublicInstanceProperties(_type).Should().HaveCount(2);
        }
    }

    public class TestType
    {
        public int Size { get; set; }
        public int Count { private get; set; }

        private bool IsHidden { get; set; }

        public void Start()
        {
        }

        private void Stop()
        {
        }
    }
}

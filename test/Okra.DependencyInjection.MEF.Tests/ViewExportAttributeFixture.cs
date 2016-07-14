using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Okra.DependencyInjection.MEF;
using Xunit;

namespace Okra.DependencyInjection.MEF.Tests
{
    public class ViewExportAttributeFixture
    {
        // *** Constructor Tests ***

        [Fact]
        public void Constructor_SetsPageName_ByString()
        {
            var attribute = new ViewExportAttribute("Page X");

            Assert.Equal("Page X", attribute.PageName);
        }

        [Fact]
        public void Constructor_SetsPageName_ByType()
        {
            var attribute = new ViewExportAttribute(typeof(ViewExportAttributeFixture));

            Assert.Equal("Okra.DependencyInjection.MEF.Tests.ViewExportAttributeFixture", attribute.PageName);
        }

        [Fact]
        public void Constructor_ThrowsException_IfPageNameIsNull()
        {
            var e = Assert.Throws<ArgumentException>(() => new ViewExportAttribute((string)null));

            Assert.Equal("The argument cannot be null or an empty string.\r\nParameter name: pageName", e.Message);
            Assert.Equal("pageName", e.ParamName);
        }

        [Fact]
        public void Constructor_ThrowsException_IfPageNameIsEmpty()
        {
            var e = Assert.Throws<ArgumentException>(() => new ViewExportAttribute(""));

            Assert.Equal("The argument cannot be null or an empty string.\r\nParameter name: pageName", e.Message);
            Assert.Equal("pageName", e.ParamName);
        }

        [Fact]
        public void Constructor_ThrowsException_IfPageTypeIsNull()
        {
            var e = Assert.Throws<ArgumentNullException>(() => new ViewExportAttribute((Type)null));

            Assert.Equal("Value cannot be null.\r\nParameter name: type", e.Message);
            Assert.Equal("type", e.ParamName);
        }

        // *** Property Tests ***

        [Fact]
        public void ContractName_IsCorrect()
        {
            var attribute = new ViewExportAttribute("Page X");

            Assert.Equal("OkraView", attribute.ContractName);
        }

        [Fact]
        public void ContractType_IsObject()
        {
            var attribute = new ViewExportAttribute("Page X");

            Assert.Equal(typeof(object), attribute.ContractType);
        }
    }
}

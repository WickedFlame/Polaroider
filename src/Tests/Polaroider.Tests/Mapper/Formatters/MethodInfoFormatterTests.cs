using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using Polaroider.Mapping.Formatters;

namespace Polaroider.Tests.Mapper.Formatters
{
	public class MethodInfoFormatterTests
	{
		[Test]
		public void MethodInfo_ctor()
		{
			Assert.DoesNotThrow(() => new MethodInfoFormatter());
		}

		[Test]
		public void MethodInfo_Format()
		{
			var method = GetMethodInfo(() => Console.WriteLine("Hello world ! {0} {1} {2}", 1, true, "test"));

			var formatter = new MethodInfoFormatter();

			Assert.That("WriteLine(System.String,System.Object,System.Object,System.Object)", Is.EqualTo(formatter.Format(method)));
		}

		[Test]
		public void MethodInfo_Format_NullParameter()
		{
			var method = GetMethodInfo(() => Console.WriteLine("Hello world ! {0} {1} {2}", 1, null, "test"));

			var formatter = new MethodInfoFormatter();

			Assert.That("WriteLine(System.String,System.Object,System.Object,System.Object)", Is.EqualTo(formatter.Format(method)));
		}

		[Test]
		public void MethodInfo_Format_InvalidObject()
		{
			var formatter = new MethodInfoFormatter();

			Assert.That("test", Is.EqualTo(formatter.Format("test")));
		}

		private static MethodInfo GetMethodInfo(Expression<Action> expression)
		{
			return ((MethodCallExpression) expression.Body).Method;
		}
	}
}

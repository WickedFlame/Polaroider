﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests.Mapper
{
	public class TypeMapperApiTests
	{
		[Test]
		public void TypeMapper_ObjectMapper()
		{
			SnapshotOptions.Default.AddMapper<CustomData>((ctx, o) =>
			{
				//only add Value and Dbl but ignore Id
				ctx.AddLine("Dbl", o.Dbl);
				ctx.AddLine("Value", o.Value);
			});

			var snapshot = new
			{
				Item = "item",
				Data = new CustomData
				{
					Id = 1,
					Dbl = 2.2,
					Value = "value",
					Inner = new InnerData
					{
						Id = 2,
						Value = "inner"
					}
				}
			}.Tokenize();

			var expected = new
			{
				Item = "item",
				Data = new
				{
					Value = "value",
					Dbl = 2.2
				}
			}.Tokenize();

			snapshot.ToString().Should().Be(expected.ToString());

			SnapshotOptions.Setup(o => { });
		}

		[Test]
		public void TypeMapper_Options_ObjectMapper()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddMapper<CustomData>((ctx, itm) =>
				{
					//only add Value and Dbl but ignore Id
					ctx.AddLine("Dbl", itm.Dbl);
					ctx.AddLine("Value", itm.Value);
				});
			});

			var snapshot = new
			{
				Item = "item",
				Data = new CustomData
				{
					Id = 1,
					Dbl = 2.2,
					Value = "value",
					Inner = new InnerData
					{
						Id = 2,
						Value = "inner"
					}
				}
			}.Tokenize(options);

			var expected = new
			{
				Data = new
				{
					Dbl = 2.2,
					Value = "value"
				},
				Item = "item",
			}.Tokenize();

			snapshot.ToString().Should().Be(expected.ToString());
		}

		[Test]
		public void TypeMapper_Options_ObjectMapper_MergeSubItems()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddMapper<CustomData>((ctx, itm) =>
				{
					//only add Value and Dbl but ignore Id
					ctx.AddLine("Inner", itm.Inner.Value);
					ctx.AddLine("Outer", itm.Value);
				});
			});

			var snapshot = new
			{
				Item = "item",
				Data = new CustomData
				{
					Id = 1,
					Dbl = 2.2,
					Value = "value",
					Inner = new InnerData
					{
						Id = 2,
						Value = "inner"
					}
				}
			}.Tokenize(options);

			var expected = new
			{
				Data = new
				{
					Outer = "value",
					Inner = "inner"
				},
				Item = "item",
			}.Tokenize();

			snapshot.ToString().Should().Be(expected.ToString());
		}

		[Test]
		public void TypeMapper_Options_ObjectMapper_DefaultMapper()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddMapper<CustomData>((ctx, itm) =>
				{
					//only add Value and Dbl but ignore Id
					ctx.Map("Inner", itm.Inner);
					ctx.AddLine("OuterValue", itm.Value);
				});
			});

			var snapshot = new
			{
				Item = "item",
				Data = new CustomData
				{
					Id = 1,
					Dbl = 2.2,
					Value = "value",
					Inner = new InnerData
					{
						Id = 2,
						Value = "inner"
					}
				}
			}.Tokenize(options);

			var expected = new
			{
				Data = new
				{
					OuterValue = "value",
					Inner = new
					{
						Id = 2,
						Value = "inner"
					}
				},
				Item = "item",
			}.Tokenize();

			snapshot.ToString().Should().Be(expected.ToString());
		}

		[Test]
		public void TypeMapper_Options_ObjectMapper_MapAnonymousObject()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddMapper<CustomData>((ctx, itm) =>
				{
					//only add Value and Dbl but ignore Id
					ctx.Map("Inner", new
					{
						InnerValue = itm.Inner.Value,
						OuterValue = itm.Value
					});
				});
			});

			var snapshot = new
			{
				Item = "item",
				Data = new CustomData
				{
					Id = 1,
					Dbl = 2.2,
					Value = "value",
					Inner = new InnerData
					{
						Id = 2,
						Value = "inner"
					}
				}
			}.Tokenize(options);

			var expected = new
			{
				Data = new
				{
					Inner = new
					{
						OuterValue = "value",
						InnerValue = "inner"
					}
				},
				Item = "item",
			}.Tokenize();

			snapshot.ToString().Should().Be(expected.ToString());
		}

		[Test]
		public void TypeMapper_Options_ObjectMapper_MapWithoutProperty()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddMapper<CustomData>((ctx, itm) =>
				{
					//only add Value and Dbl but ignore Id
					ctx.Map(new
					{
						InnerValue = itm.Inner.Value,
						OuterValue = itm.Value
					});
				});
			});

			var snapshot = new
			{
				Item = "item",
				Data = new CustomData
				{
					Id = 1,
					Dbl = 2.2,
					Value = "value",
					Inner = new InnerData
					{
						Id = 2,
						Value = "inner"
					}
				}
			}.Tokenize(options);

			var expected = new
			{
				Data = new
				{
					OuterValue = "value",
					InnerValue = "inner"
				},
				Item = "item",
			}.Tokenize();

			snapshot.ToString().Should().Be(expected.ToString());
		}


		[Test]
		public void TypeMapper_Options_ObjectMapper_RootObject()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddMapper<CustomData>((ctx, itm) =>
				{
					//only add Value and Dbl but ignore Id
					ctx.Map(new
					{
						InnerValue = itm.Inner.Value,
						OuterValue = itm.Value
					});
				});
			});

			var snapshot = new CustomData
			{
				Id = 1,
				Dbl = 2.2,
				Value = "value",
				Inner = new InnerData
				{
					Id = 2,
					Value = "inner"
				}
			}.Tokenize(options);

			var expected = new
			{
				OuterValue = "value",
				InnerValue = "inner"
			}.Tokenize();

			snapshot.ToString().Should().Be(expected.ToString());
		}

		[Test]
		public void TypeMapper_Options_MapInnerClass()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddMapper<InnerData>((ctx, itm) =>
				{
					// only mapp the id
					ctx.AddLine("Id", itm.Id);
				});
				o.AddMapper<CustomData>((ctx, itm) =>
				{
					// only add Value and Dbl but ignore Id
					ctx.Map("Inner", itm.Inner);
					ctx.AddLine("OuterValue", itm.Value);
				});
			});

			var snapshot = new
			{
				Item = "item",
				Data = new CustomData
				{
					Id = 1,
					Dbl = 2.2,
					Value = "value",
					Inner = new InnerData
					{
						Id = 2,
						Value = "inner"
					}
				}
			}.Tokenize(options);

			var expected = new
			{
				Data = new
				{
					OuterValue = "value",
					Inner = new
					{
						Id = 2
					}
				},
				Item = "item",
			}.Tokenize();

			snapshot.ToString().Should().Be(expected.ToString());
		}

		[Test]
		public void TypeMapper_Options_AddMapper_InMap()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddMapper<CustomData>((ctx, itm) =>
				{
					// only add Value and Dbl but ignore Id
					o.AddMapper<InnerData>((ctx, itm) =>
					{
						// only mapp the id
						ctx.AddLine("Id", itm.Id);
					});

					// only add Value and Dbl but ignore Id
					ctx.Map("Inner", itm.Inner);
					ctx.AddLine("OuterValue", itm.Value);
				});
			});

			var snapshot = new
			{
				Item = "item",
				Data = new CustomData
				{
					Id = 1,
					Dbl = 2.2,
					Value = "value",
					Inner = new InnerData
					{
						Id = 2,
						Value = "inner"
					}
				}
			}.Tokenize(options);

			var expected = new
			{
				Data = new
				{
					OuterValue = "value",
					Inner = new
					{
						Id = 2
					}
				},
				Item = "item",
			}.Tokenize();

			snapshot.ToString().Should().Be(expected.ToString());
		}

		[Test]
		public void TypeMapper_Options_OverrideMapper()
		{
			var options = SnapshotOptions.Create(o =>
			{
				o.AddMapper<InnerData>((ctx, itm) =>
				{
					// only mapp the id
					ctx.AddLine("Value", itm.Value);
				});

				o.AddMapper<CustomData>((ctx, itm) =>
				{
					// overrid to only add Value and Dbl but ignore Id
					o.AddMapper<InnerData>((ctx2, itm2) =>
					{
						// only mapp the id
						ctx2.AddLine("Id", itm2.Id);
					});

					// only add Value and Dbl but ignore Id
					ctx.Map("Inner", itm.Inner);
					ctx.AddLine("OuterValue", itm.Value);
				});
			});

			var snapshot = new
			{
				Item = "item",
				Data = new CustomData
				{
					Id = 1,
					Dbl = 2.2,
					Value = "value",
					Inner = new InnerData
					{
						Id = 2,
						Value = "inner"
					}
				}
			}.Tokenize(options);

			var expected = new
			{
				Data = new
				{
					OuterValue = "value",
					Inner = new
					{
						Id = 2
					}
				},
				Item = "item",
			}.Tokenize();

			snapshot.ToString().Should().Be(expected.ToString());
		}





		public class CustomData
		{
			public int Id { get; set; }

			public double Dbl { get; set; }

			public string Value { get; set; }

			public InnerData Inner { get; set; }
		}

		public class InnerData
		{
			public int Id { get; set; }
			
			public string Value { get; set; }
		}
	}
}

﻿namespace DeepEqual.Syntax
{
	using System;
	using System.Diagnostics.Contracts;
	using System.Linq.Expressions;

	public class CompareSyntax<TActual, TExpected> : IComparisonBuilder<CompareSyntax<TActual, TExpected>>
	{
		public TActual Actual { get; set; }
		public TExpected Expected { get; set; }

		public IComparisonBuilder<ComparisonBuilder> Builder { get; set; }

		public CompareSyntax(TActual actual, TExpected expected)
		{
			Actual = actual;
			Expected = expected;
			Builder = new ComparisonBuilder();
		}

		[Pure]
		public CompareSyntax<TActual, TExpected> IgnoreSourceProperty(Expression<Func<TActual, object>> property)
		{
			Builder.IgnoreProperty(property);
			return this;
		}

		[Pure]
		public CompareSyntax<TActual, TExpected> IgnoreDestinationProperty(Expression<Func<TExpected, object>> property)
		{
			Builder.IgnoreProperty(property);
			return this;
		}

		[Pure]
		public CompareSyntax<TActual, TExpected> IgnoreProperty<T>(Expression<Func<T, object>> property)
		{
			Builder.IgnoreProperty(property);
			return this;
		}

		[Pure]
		public CompareSyntax<TActual, TExpected> IgnoreProperty(Func<PropertyReader, bool> func)
		{
			Builder.IgnoreProperty(func);
			return this;
		}

		[Pure]
		public CompareSyntax<TActual, TExpected> SkipDefault<T>()
		{
			Builder.SkipDefault<T>();
			return this;
		}

		[Pure]
		public CompareSyntax<TActual, TExpected> WithCustomComparison(IComparison comparison)
		{
			Builder.WithCustomComparison(comparison);
			return this;
		}

		[Pure]
		public CompareSyntax<TActual, TExpected> IgnoreUnmatchedProperties()
		{
			Builder.IgnoreUnmatchedProperties();
			return this;
		}

		[Pure]
		public CompareSyntax<TActual, TExpected> ExposeInternalsOf<T>()
		{
			Builder.ExposeInternalsOf<T>();
			return this;
		}

		[Pure]
		public CompareSyntax<TActual, TExpected> ExposeInternalsOf(params Type[] types)
		{
			Builder.ExposeInternalsOf(types);
			return this;
		}

		[Pure]
		public bool Compare()
		{
			return Actual.IsDeepEqual(Expected, Builder.Create());
		}

		public void Assert()
		{
			Actual.ShouldDeepEqual(Expected, Builder.Create());
		}

		//ncrunch: no coverage start
		CompositeComparison IComparisonBuilder<CompareSyntax<TActual, TExpected>>.Create()
		{
			throw new NotImplementedException();
		}
		//ncrunch: no coverage end
	}
}
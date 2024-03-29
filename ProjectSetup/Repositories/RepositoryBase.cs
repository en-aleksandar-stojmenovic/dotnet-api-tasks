﻿using Microsoft.EntityFrameworkCore;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProjectSetup.Repositories
{
	public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected ApplicationDbContext _context { get; set; }

		public RepositoryBase(ApplicationDbContext context)
		{
			this._context = context;
		}
		public IQueryable<T> FindAll()
		{
			return this._context.Set<T>().AsNoTracking();
		}
		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
		{
			return this._context.Set<T>()
				.Where(expression).AsNoTracking();
		}
		public void Create(T entity)
		{
			this._context.Set<T>().Add(entity);
		}
		public void Update(T entity)
		{
			this._context.Set<T>().Update(entity);
		}
		public void Delete(T entity)
		{
			this._context.Set<T>().Remove(entity);
		}
	}
}

﻿using Microsoft.EntityFrameworkCore;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Data;
using ProjectSetup.Domain;
using ProjectSetup.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSetup.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Post>> FindAllPostsAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<Post> FindPostByIdAsync(Guid id)
        {
			var post = await FindByCondition(post => post.Id.Equals(id))
						.FirstOrDefaultAsync();

			if (post == null)
			{
				throw new PostNotFoundException("Post with Id: '" + id + "' not found");
			}

			return post;
        }

        public async Task<Post> CreatePost(CreatePostRequest postRequest)
        {
			if (await _context.Categories.FindAsync(postRequest.CategoryId) == null)
			{
				throw new CategoryBadRequestException("Category with Id: '" + postRequest.CategoryId + "' not found");
			}

			var post = new Post
			{
				Id = Guid.NewGuid(),
				Text = postRequest.Text,
				Created = DateTime.Now,
				CategoryId = postRequest.CategoryId,
				CreatedBy = postRequest.CreatedById
			};

			Create(post);

			return post;
        }

        public async Task<Post> UpdatePost(UpdatePostRequest postRequest)
        {
			Post post = await FindByCondition(post => post.Id.Equals(postRequest.Id))
						.FirstOrDefaultAsync();

			if (post == null)
			{
				throw new PostBadRequestException("Post with Id: '" + postRequest.Id + "' not found");
			}

			if (await _context.Categories.FindAsync(postRequest.CategoryId) == null)
			{
				throw new CategoryBadRequestException("Category with Id: '" + postRequest.CategoryId + "' not found");
			}

			post.CategoryId = postRequest.CategoryId;
			post.Text = postRequest.Text;

			Update(post);

			return post;
        }
		public void DeletePost(Guid postId)
        {
			var post = FindByCondition(post => post.Id.Equals(postId))
						.FirstOrDefaultAsync().Result;

			if (post == null)
			{
				throw new PostBadRequestException("Post with Id: '" + postId + "' not found");
			}

			Delete(post);
        }
    }
}

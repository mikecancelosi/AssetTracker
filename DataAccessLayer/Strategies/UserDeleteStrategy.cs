﻿using DomainModel;
using System.Linq;

namespace DataAccessLayer.Strategies
{
    public class UserDeleteStrategy : IDeleteStrategy<User>
    {
        public void Delete(GenericUnitOfWork uow, User item)
        {
            GenericRepository<User> userRepo = uow.GetRepository<User>();

            foreach(Asset asset in item.AssetsAssigned.ToList())
            {
                asset.as_usid = null;
            }

            foreach(Discussion post in item.Discussions.ToList())
            {
                post.di_usid = null;
            }

            userRepo.Delete(item);

        }
    }
}

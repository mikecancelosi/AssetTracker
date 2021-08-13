﻿using DataAccessLayer;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());

            PermissionsBuilder.BuildFile(uow);
        }
    }
}

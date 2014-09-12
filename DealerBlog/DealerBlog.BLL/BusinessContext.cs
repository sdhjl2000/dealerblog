using System;
using System.Collections.Generic;



namespace DealerBlog.BLL
{
    public  delegate object InitTypeInstance(Type t);
    
    public class BusinessContext
    {
            public static ICategory CategoryService;            
            public static IPost PostService;            
            public static IPostTagMap PostTagMapService;            
            public static ITag TagService;            
        
        
        public static InitTypeInstance InitEvent;

        public static void InitContext()
        {
            if (InitEvent != null)
            {
                    CategoryService = InitEvent(typeof(ICategory)) as ICategory;         
                    PostService = InitEvent(typeof(IPost)) as IPost;         
                    PostTagMapService = InitEvent(typeof(IPostTagMap)) as IPostTagMap;         
                    TagService = InitEvent(typeof(ITag)) as ITag;         
            }
        }
    }
}
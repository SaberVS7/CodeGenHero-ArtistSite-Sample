// <auto-generated> - Template:APIControllerCustom, Version:2022.06.09, Id:44d5b085-471a-4f79-9440-4254c967f282
using ArtistSite.Repository.Infrastructure;
using ArtistSite.Repository.Repositories;
using entAS = ArtistSite.Repository.Entities;
using Enums = ArtistSite.Shared.Constants.Enums;

namespace ArtistSite.Api.Controllers
{
	public partial class NewsItemController
	{
		protected IASRepository NewsItemRepository { get; set; }
		protected override void RunCustomLogicAfterCtor()
		{
			NewsItemRepository = ServiceProvider.GetService<IASRepository>();
		}

		partial void RunCustomLogicAfterInsert(ref entAS.NewsItem newDBItem, ref IRepositoryActionResult<entAS.NewsItem> result)
		{

		}

		partial void RunCustomLogicAfterUpdatePatch(ref entAS.NewsItem updatedDBItem, ref IRepositoryActionResult<entAS.NewsItem> result)
		{

		}

		partial void RunCustomLogicAfterUpdatePut(ref entAS.NewsItem updatedDBItem, ref IRepositoryActionResult<entAS.NewsItem> result)
		{

		}

		partial void RunCustomLogicBeforeUpdatePut(ref entAS.NewsItem updatedDBItem, int newsItemId)
		{

		}

		partial void RunCustomLogicOnGetEntityByPK(ref entAS.NewsItem dbItem, int newsItemId, Enums.RelatedEntitiesType relatedEntitiesType)
		{

		}

	}
}
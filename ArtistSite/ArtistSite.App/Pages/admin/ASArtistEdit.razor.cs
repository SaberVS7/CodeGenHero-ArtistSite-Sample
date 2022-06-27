// <auto-generated> - Template:AdminEditViewModel, Version:2021.11.12, Id:17ae856a-a589-40c0-a5be-1579b0714385
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using ArtistSite.App.Services;
using ArtistSite.App.Shared;
using ArtistSite.Shared.Constants;
using ArtistSite.Shared.DTO;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ArtistSite.App.Pages
{
	[Authorize(Roles = Consts.ROLE_ADMIN_OR_USER)]
	public partial class ASArtistEditViewModel : AdminPageBase
	{
		public Artist Artist { get; set; } = new Artist();

		[Inject]
		public IWebApiDataServiceAS WebApiDataServiceAS { get; set; }

		[Parameter]
		public int ArtistId { get; set; }

		protected override async Task OnInitializedAsync()
		{
				await base.OnInitializedAsync();

				List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>()
				{
						new BreadcrumbItem("Home", "/"),
						new BreadcrumbItem("List of Artists", "/admin/artists"),
						new BreadcrumbItem("Edit Artist", $"/admin/artistedit/{ArtistId}", disabled: true)
				};

				NavigationService.SetBreadcrumbs(breadcrumbs);
		}

		protected async override Task OnParametersSetAsync()
		{
				IsReady = false;
				await SetSavedAsync(false);

				try
				{
						if (false) // Populate with what this entity's Default State indicating a new item being created is
						{
								// Define entity defaults
								Artist = new Artist { };
						}
						else
						{
								if (Artist == null || Artist.ArtistId != ArtistId)
								{
										var result = await WebApiDataServiceAS.GetArtistAsync(ArtistId);
										if (result.IsSuccessStatusCode)
										{
												var artist = result.Data;
												// Admins and other approved user claims (Add below) only
												if (!User.IsInRole(Consts.ROLE_ADMIN))
												{
														NavigationManager.NavigateTo($"/Authorization/AccessDenied");
												}
												else
												{
														Artist = artist;
												}
										}
								}
						}
				}
				finally
				{
						IsReady = true;
				}
		}

		protected async Task OnValidSubmit()
		{
				await SetSavedAsync(false);

				ClearNoneValues();

				if (false) // Populate with what this entity's Default State indicating a new item being created is
				{
						var result = await WebApiDataServiceAS.CreateArtistAsync(Artist);
						if (result.IsSuccessStatusCode)
						{
								Artist = result.Data;
								StatusClass = "alert-success";
								Message = "New item added successfully.";
								await SetSavedAsync(true);
						}
						else
						{
								StatusClass = "alert-danger";
								Message = "Something went wrong adding the new item. Please try again.";
								await SetSavedAsync(false);
						}
				}
				else
				{
						var result = await WebApiDataServiceAS.UpdateArtistAsync(Artist);
						if (result.IsSuccessStatusCode)
						{
								StatusClass = "alert-success";
								Message = "Artist updated successfully.";
								await SetSavedAsync(true);
						}
						else
						{
								StatusClass = "alert-danger";
								Message = "Something went wrong updating the new item. Please try again.";
								await SetSavedAsync(false);
						}
				}
		}

		protected void ReturnToList()
		{
				NavigationManager.NavigateTo("/admin/artists");
		}

        protected async Task SetSavedAsync(bool value)
        {
            Saved = value;
            if (value == true)
            {
                await JsRuntime.InvokeVoidAsync("blazorExtensions.ScrollToTop");
            }
        }

        private void ClearNoneValues()
        {
            // Add handling for null values here
        }
	}
}


using AutoMapper;
using Domain.Entities;
using Shared.Fooditem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Servcies.MappingProfile
{
    public class FoodItemProfile : Profile
    {
        public FoodItemProfile()
        {
            // =========================
            // Create DTO -> Entity
            // =========================
            CreateMap<CreateFoodItemDto, FoodItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Restaurant, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            // =========================
            // Update DTO -> Entity
            // =========================
            CreateMap<UpdateFoodItemDto, FoodItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RestaurantId, opt => opt.Ignore()) // مهم جدًا
                .ForMember(dest => dest.Restaurant, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            // =========================
            // Entity -> GetAll DTO
            // =========================
            CreateMap<FoodItem, FoodItemDto>()
                .ForMember(dest => dest.RestaurantName,
                    opt => opt.MapFrom(src => src.Restaurant.Name))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name));

            // =========================
            // Entity -> Details DTO
            // =========================
            CreateMap<FoodItem, FoodItemDetailsDto>()
                .ForMember(dest => dest.RestaurantName,
                    opt => opt.MapFrom(src => src.Restaurant.Name))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name));

            // =========================
            // Reverse Mapping (اختياري)
            // =========================
            CreateMap<FoodItemDto, FoodItem>();
        }
    }
}

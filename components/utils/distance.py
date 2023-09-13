import json
from math import radians, sin, cos, sqrt, atan2


def calculate_distance(lat1, lon1, lat2, lon2):
    R = 6371.0  # Earth radius in kilometers

    lat1, lon1 = radians(lat1), radians(lon1)
    lat2, lon2 = radians(lat2), radians(lon2)

    dlat = lat2 - lat1
    dlon = lon2 - lon1

    a = sin(dlat / 2)**2 + cos(lat1) * cos(lat2) * sin(dlon / 2)**2
    c = 2 * atan2(sqrt(a), sqrt(1 - a))

    return R * c


with open("../../libs/Profio.Infrastructure/Persistence/Seeding/Hub.json",
          "r",
          encoding="utf-8") as file:
    hub_data = json.load(file)

provinces_from_data = {[hub["Address"]["Province"] for hub in hub_data]}

neighbors = {
    "Hà Nội": ["Hà Nam", "Hưng Yên", "Bắc Ninh", "Vĩnh Phúc", "Hòa Bình"],
    "TP Hồ Chí Minh":
    ["Bình Dương", "Đồng Nai", "Bà Rịa - Vũng Tàu", "Tây Ninh", "Long An"],
    "Đà Nẵng": ["Quảng Nam", "Thừa Thiên Huế"],
    "Hải Phòng": ["Quảng Ninh", "Hải Dương", "Thái Bình"],
    "Cần Thơ": [
        "Vĩnh Long", "Hậu Giang", "An Giang", "Kiên Giang", "Đồng Tháp",
        "Sóc Trăng"
    ],
    "Lào Cai": ["Yên Bái", "Phú Thọ", "Hà Giang"],
    "Yên Bái": ["Lào Cai", "Phú Thọ", "Sơn La", "Tuyên Quang"],
    "Bắc Giang": ["Bắc Ninh", "Hà Nội", "Quảng Ninh", "Lạng Sơn"],
    "Quảng Ninh": ["Hải Phòng", "Bắc Giang", "Lạng Sơn"],
    "Hải Dương": ["Hà Nội", "Hưng Yên", "Hải Phòng", "Bắc Ninh"],
    "Ninh Bình": ["Nam Định", "Hà Nam", "Thanh Hóa"],
    "Quảng Ngãi": ["Bình Định", "Quảng Nam"],
    "Bình Định": ["Phú Yên", "Quảng Ngãi"],
    "Gia Lai": ["Kon Tum", "Đắk Lắk", "Phú Yên"],
    "Đắk Lắk": ["Đắk Nông", "Lâm Đồng", "Gia Lai"],
    "Đắk Nông": ["Đắk Lắk", "Bình Phước", "Lâm Đồng"],
    "Bình Phước": ["Đồng Nai", "Bình Dương", "Đắk Nông"],
    "Bình Thuận": ["Lâm Đồng", "Ninh Thuận", "Đồng Nai"],
    "Vĩnh Long": ["Cần Thơ", "Tiền Giang", "Đồng Tháp"],
    "Bến Tre": ["Tiền Giang", "Trà Vinh"],
    "An Giang": ["Cần Thơ", "Kiên Giang", "Đồng Tháp"],
    "Sóc Trăng": ["Bạc Liêu", "Cần Thơ", "Hậu Giang"],
    "Bạc Liêu": ["Cà Mau", "Sóc Trăng"],
    "Cà Mau": ["Kiên Giang", "Bạc Liêu"]
}

hubs_by_province = {hub["Address"]["Province"]: hub for hub in hub_data}

distances = []
for province, neighbors_list in neighbors.items():
    for neighbor in neighbors_list:
        if province in hubs_by_province
        and neighbor in hubs_by_province
        and province != neighbor:
            start_hub = hubs_by_province[province]
            end_hub = hubs_by_province[neighbor]
            distance = calculate_distance(start_hub["Location"]["Latitude"],
                                          start_hub["Location"]["Longitude"],
                                          end_hub["Location"]["Latitude"],
                                          end_hub["Location"]["Longitude"])
            distances.append({
                "Distance": round(distance, 2),
                "StartHubId": start_hub["Id"],
                "EndHubId": end_hub["Id"]
            })

print(distances)

with open("../../libs/Profio.Infrastructure/Persistence/Seeding/Route.json",
          "w",
          encoding="utf-8") as file:
    json.dump(distances, file, ensure_ascii=False, indent=4)

print("Done")

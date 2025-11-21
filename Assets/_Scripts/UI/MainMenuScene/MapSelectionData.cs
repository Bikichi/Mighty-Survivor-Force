//Static được khởi tạo 1 lần duy nhất khi bạn lần đầu tiên truy cập vào class đó và dữ liệu static được lưu chung.
//Static tồn tại xuyên suốt vòng đời của chương trình
//Thuộc về class, không thuộc về object (instance)
//Các các thành phần bên trong class static đều phải static, không tạo đối tượng từ static class
public static class MapSelectionData
{
    public static int currentIndex = 0;
    public static MapData[] maps;
}
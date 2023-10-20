import os

def rename_items_in_directory(directory_path):
    """
    Rename files and folders in the directory by replacing "Frecuency" with "Frequency".
    :param directory_path: The path of the directory where files/folders need to be renamed.
    """
    done = False;

    try:
        for item_name in os.listdir(directory_path):
            if done is False:
                done = True;
                if "Frequencies" in item_name:
                    directory_path = directory_path + f'\{item_name}'
                    for file_name in os.listdir():
                        new_name = item_name.replace("Frecuencies", "Frequencies")
                        old_path = os.path.join(directory_path, item_name)
                        new_path = os.path.join(directory_path, new_name)
                        
                        # Rename the file or folder
                        os.rename(old_path, new_path)
                    print(f'Renamed {old_path} to {new_path}')
            if "Frecuencies" in item_name:
                new_name = item_name.replace("Frecuencies", "Frequencies")
                old_path = os.path.join(directory_path, item_name)
                new_path = os.path.join(directory_path, new_name)
                
                # Rename the file or folder
                os.rename(old_path, new_path)
                print(f'Renamed {old_path} to {new_path}')

    except Exception as e:
        print(f"An error occurred in {directory_path}: {e}")

# Las rutas base
routes = [
    r'C:\Users\USER\source\repos\USBMED\SoftwareDesign\Indicators\api\src\IndicatorsApi.Domain\Features',
    r'C:\Users\USER\source\repos\USBMED\SoftwareDesign\Indicators\api\src\IndicatorsApi.Application\Features',
    r'C:\Users\USER\source\repos\USBMED\SoftwareDesign\Indicators\api\src\IndicatorsApi.Persistence\Features',
    r'C:\Users\USER\source\repos\USBMED\SoftwareDesign\Indicators\api\src\IndicatorsApi.Contracts',
    r'C:\Users\USER\source\repos\USBMED\SoftwareDesign\Indicators\api\src\IndicatorsApi.Presentation\Features',
    r'C:\Users\USER\source\repos\USBMED\SoftwareDesign\Indicators\api\src\IndicatorsApi.WebApi\Features',
]

for route in routes:
    rename_items_in_directory(route)

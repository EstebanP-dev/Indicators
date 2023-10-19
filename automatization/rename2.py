import os

def rename_items_in_directory(directory_path):
    """
    Rename files and folders in the directory by replacing "Frecuency" with "Frequency".
    :param directory_path: The path of the directory where files/folders need to be renamed.
    """
    try:
        for item_name in os.listdir(directory_path):
            if "Frecuency" in item_name:
                new_name = item_name.replace("Frecuency", "Frequency")
                old_path = os.path.join(directory_path, item_name)
                new_path = os.path.join(directory_path, new_name)
                
                # Rename the file or folder
                os.rename(old_path, new_path)
                print(f'Renamed {old_path} to {new_path}')

    except Exception as e:
        print(f"An error occurred: {e}")

if __name__ == "__main__":

    routes = [
        r'/Users/juanestebannaviaperez/Documents/Personal/repos/USBMED/Indicators/api/src/IndicatorsApi.Domain/Features',
        r'/Users/juanestebannaviaperez/Documents/Personal/repos/USBMED/Indicators/api/src/IndicatorsApi.Application/Features',
        r'/Users/juanestebannaviaperez/Documents/Personal/repos/USBMED/Indicators/api/src/IndicatorsApi.Persistence/Features',
        r'/Users/juanestebannaviaperez/Documents/Personal/repos/USBMED/Indicators/api/src/IndicatorsApi.Contracts',
        r'/Users/juanestebannaviaperez/Documents/Personal/repos/USBMED/Indicators/api/src/IndicatorsApi.Presentation/Features',
        r'/Users/juanestebannaviaperez/Documents/Personal/repos/USBMED/Indicators/api/src/IndicatorsApi.WebApi/Features',
    ],

    for route in routes:
        directoryPath = route.__str__()
        if os.path.isdir(directoryPath):
            rename_items_in_directory(directoryPath)
        else:
            print("Provided path is not a directory.")

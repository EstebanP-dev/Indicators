import os
import re
import tempfile

search_string = "ActorType"
search_dbname = "tipoactor"
replace_string = "IndicatorType"
replace_path = "IndicatorTypes"
replace_dbname = "tipoindicador"

routes = {
    "Api": [
        r'C:\Users\USER\source\repos\USBMED\SoftwareDesign\Indicators\api\src\IndicatorsApi.Domain\Features',
        r'C:\Users\USER\source\repos\USBMED\SoftwareDesign\Indicators\api\src\IndicatorsApi.Application\Features',
        r'C:\Users\USER\source\repos\USBMED\SoftwareDesign\Indicators\api\src\IndicatorsApi.Persistence\Features',
        r'C:\Users\USER\source\repos\USBMED\SoftwareDesign\Indicators\api\src\IndicatorsApi.Contracts',
        r'C:\Users\USER\source\repos\USBMED\SoftwareDesign\Indicators\api\src\IndicatorsApi.Presentation\Features',
        r'C:\Users\USER\source\repos\USBMED\SoftwareDesign\Indicators\api\src\IndicatorsApi.WebApi\Features',
    ],
    "Web": []
}

pattern = r'(\w+)' + search_string + r'(\w+)'  # Expresión regular para buscar "GetAction", "CreateAction", etc.

def rename_file_or_folder(path):
    # Obtiene el nombre del archivo o carpeta sin la ruta
    base_name = os.path.basename(path)
    
    # Verifica si el nombre contiene la cadena a buscar
    if search_string in base_name:
        # Reemplaza la cadena en el nombre
        new_name = base_name.replace(search_string, replace_string)
        
        # Obtiene la ruta del directorio contenedor
        parent_dir = os.path.dirname(path)
        
        # Construye el nuevo camino completo
        new_path = os.path.join(parent_dir, new_name)
        
        # Renombra el archivo o carpeta
        os.rename(path, new_path)

for folder_path in routes['Api']:

    folder_path = folder_path + r'\\' + replace_path

    for subdir, _, files in os.walk(folder_path):
        for filename in files:
            
            file_path = os.path.join(subdir, filename)
            new_name = re.sub(pattern, r'\1' + replace_string + r'\2', filename)
            
            if new_name != filename:

                if file_path.endswith(".cs"):
                    with open(file_path) as f:
                        content = f.read()

                    search_lower_string: str = search_string[0].lower() + search_string[1::]
                    replace_lower_string: str = replace_string[0].lower() + replace_string[1::]
                    
                    content = content.replace(search_string, replace_string)
                    content = content.replace(search_lower_string, replace_lower_string)
                    content = content.replace(search_dbname, replace_dbname)

                    temp_file_path = os.path.join(tempfile.mkdtemp(), filename)

                    with open(temp_file_path, 'w') as f:
                        f.write(content)
                    
                    os.replace(temp_file_path, file_path)
        
    for root, dirs, files in os.walk(folder_path):
        for dir_name in dirs:
            dir_path = os.path.join(root, dir_name)
            rename_file_or_folder(dir_path)

        for file_name in files:
            file_path = os.path.join(root, file_name)
            rename_file_or_folder(file_path)

print("Reemplazo completado.")
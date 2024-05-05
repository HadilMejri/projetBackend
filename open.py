import cv2
import pytesseract
from PIL import Image
import os

def extract_text_from_image(image_path, output_directory):
    # Charger l'image d'entrée
    image = cv2.imread(image_path)

    # Convertir l'image en mode gris
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

    # Appliquer un filtre médian pour réduire le bruit
    gray = cv2.medianBlur(gray, 3)

    # Appliquer une binarisation adaptative pour améliorer le contraste
    gray = cv2.adaptiveThreshold(gray, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY, 11, 2)

    # Inverser les couleurs de l'image
    gray = cv2.bitwise_not(gray)

    # Extraire le texte de l'image
    text = pytesseract.image_to_string(image, lang='fra', config='--psm 6')

    # Enregistrer le texte extrait dans un fichier texte
    output_text_path = os.path.join(output_directory, 'CTN471.txt')
    with open(output_text_path, 'w') as f:
        f.write(text)

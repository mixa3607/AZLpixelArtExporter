require("DecompOrigSrc/activity_coloring_template")
json = require("Libs/json")
coloringTemplatesExport = {}

for currentTemplateId,currentTemplate in ipairs(pg.activity_coloring_template) do
  coloringTemplateExport = {id = currentTemplate.id, 
    name = currentTemplate.name, 
    blank = currentTemplate.blank,
    length = currentTemplate.theme[2],
    height = currentTemplate.theme[1],
    colorIds = currentTemplate.color_id_list,
    colors = {},
    cells = {} }
  for currentCellId,currentCell in ipairs(currentTemplate.cells) do
    coloringTemplateExport.cells[currentCellId] = {x = currentCell[2], y = currentCell[1], colorNum = currentCell[3]}
  end
  for currentColorId,currentColor in ipairs(currentTemplate.colors) do
    coloringTemplateExport.colors[currentColorId] = 
		math.floor(currentColor[4]*255) .. "," 
		.. math.floor(currentColor[1]*255) .. "," 
		.. math.floor(currentColor[2]*255) .. "," 
		.. math.floor(currentColor[3]*255)
		--bgra lua(unity3d) <=> argb C#
  end
    
  coloringTemplatesExport[currentTemplate.id] = coloringTemplateExport
end

coloringTemplatesExportJson = json.encode(coloringTemplatesExport)
--print(coloringTemplatesExportJson)

return coloringTemplatesExportJson
import {useEffect, useState} from "react"
import {ProductType} from "../types/ProductTypes.ts";

const useProducts = () => {
    const [products, setProducts] = useState<ProductType[]>([])
    const [productsLoading, setProductsLoading] = useState(false)
    
    useEffect(() => {
        (async () => {
            try {
                setProductsLoading(true)
                const response = await fetch('http://localhost:5022/api/Product')
                const prods = await response.json()
                setProducts(prods)
            }
            catch(err) {
                console.error(err)
            }
            finally {
                setProductsLoading(false)
            }
        })()
    }, [])
    
    return {products, productsLoading}
}

export default useProducts